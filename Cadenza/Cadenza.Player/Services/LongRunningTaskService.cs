namespace Cadenza.Player;

public interface ILongRunningTaskService
{
    event TaskGroupProgressEventHandler TaskGroupProgressChanged;
    event SubTaskProgressEventHandler SubTaskProgressChanged;

    Task RunTasks(TaskGroup taskGroup, CancellationToken cancellationToken);
}

public class LongRunningTaskService : ILongRunningTaskService
{
    public event TaskGroupProgressEventHandler TaskGroupProgressChanged;
    public event SubTaskProgressEventHandler SubTaskProgressChanged;

    public async Task RunTasks(TaskGroup taskGroup, CancellationToken cancellationToken)
    {
        try
        {
            Update("Starting", cancellationToken);

            if (taskGroup.PreTask != null)
            {
                Update(taskGroup.PreTask.Caption, cancellationToken);
                await taskGroup.PreTask.Task;
            }

            Update("Running", cancellationToken);

            var tasks = new List<Task>();

            foreach (var task in taskGroup.Tasks)
            {
                tasks.Add(PerformTask(task, cancellationToken));
            }

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.WhenAll(tasks).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    TaskGroupProgressChanged?.Invoke(this, new TaskGroupProgressEventArgs { Message = "Errored", Errored = true });
                }
                else if (t.IsCanceled)
                {
                    Update("Cancelling", CancellationToken.None);
                    TaskGroupProgressChanged?.Invoke(this, new TaskGroupProgressEventArgs { Message = "Cancelled", Cancelled = true });
                }
                else
                {
                    TaskGroupProgressChanged?.Invoke(this, new TaskGroupProgressEventArgs { Message = "Completed", Completed = true });
                }
            });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            if (taskGroup.PostTask != null)
            {
                Update(taskGroup.PostTask.Caption, cancellationToken);
                await taskGroup.PostTask.Task;
            }
        }
        catch (OperationCanceledException)
        {
            Update("Cancelling", CancellationToken.None);
            TaskGroupProgressChanged?.Invoke(this, new TaskGroupProgressEventArgs { Message = "Cancelled", Cancelled = true });
        }
        catch (Exception ex)
        {

        }
    }

    private async Task PerformTask(SubTask task, CancellationToken cancellation)
    {
        try
        {
            foreach (var step in task.Steps)
            {
                Update(task.Id, step.Caption, cancellation);
                await step.Task;
            }

            SubTaskProgressChanged?.Invoke(this, new SubTaskProgressEventArgs
            {
                Id = task.Id,
                Message = "Completed",
                Completed = true
            });
        }
        catch (OperationCanceledException)
        {
            SubTaskProgressChanged?.Invoke(this, new SubTaskProgressEventArgs
            {
                Id = task.Id,
                Message = "Cancelled",
                Cancelled = true
            });

            throw;
        }
    }

    private void Update(string message, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        TaskGroupProgressChanged?.Invoke(this, new TaskGroupProgressEventArgs { Message = message });
    }

    private void Update(string id, string message, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        SubTaskProgressChanged?.Invoke(this, new SubTaskProgressEventArgs { Id = id, Message = message });
    }
}

public class TaskStep
{
    public string Caption { get; set; }
    public Task Task { get; set; }
}

public class SubTask
{
    public string Id { get; set; }
    public string Title { get; set; }
    public List<TaskStep> Steps { get; set; } = new List<TaskStep>();

    public void AddStep(string caption, Task task)
    {
        Steps.Add(new TaskStep { Caption = caption, Task = task });
    }
}

public class TaskGroup
{
    public string Title { get; set; }
    public List<SubTask> Tasks { get; set; } = new List<SubTask>();
    public TaskStep PreTask { get; set; }
    public TaskStep PostTask { get; set; }
}

public enum TaskState
{
    None,
    Running,
    Cancelling,
    Cancelled,
    Completed,
    Errored
}
