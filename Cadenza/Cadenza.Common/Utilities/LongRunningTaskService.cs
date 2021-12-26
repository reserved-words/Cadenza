namespace Cadenza.Common;

public class LongRunningTaskService : ILongRunningTaskService
{
    public event TaskGroupProgressEventHandler TaskGroupProgressChanged;
    public event SubTaskProgressEventHandler SubTaskProgressChanged;

    public async Task RunTasks(TaskGroup taskGroup, CancellationToken cancellationToken)
    {
        try
        {
            if (taskGroup.PreTask != null)
            {
                Update(TaskState.Starting, cancellationToken);
                await taskGroup.PreTask();
            }

            Update(TaskState.Running, cancellationToken);

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
                    Update(TaskState.Errored, CancellationToken.None);
                }
                else if (t.IsCanceled)
                {
                    Update(TaskState.Cancelling, CancellationToken.None);
                    // Add any cancellation tasks
                    Update(TaskState.Cancelled, CancellationToken.None);
                }
                else
                {
                    Update(TaskState.Completed, CancellationToken.None);
                }
            });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
        catch (OperationCanceledException)
        {
            Update(TaskState.Cancelling, CancellationToken.None);
            // Add any cancellation tasks
            Update(TaskState.Cancelled, CancellationToken.None);
        }
        catch (Exception ex)
        {
            // Add any cancellation tasks
            Update(TaskState.Errored, CancellationToken.None);
        }
    }

    private async Task PerformTask(SubTask task, CancellationToken cancellationToken)
    {
        try
        {
            foreach (var step in task.Steps)
            {
                Update(task.Id, step.Caption, TaskState.Running, cancellationToken);
                await step.Task();
            }

            Update(task.Id, "Completed", TaskState.Completed, CancellationToken.None);
        }
        catch (OperationCanceledException)
        {
            Update(task.Id, "Cancelled", TaskState.Cancelled, CancellationToken.None);
            throw;
        }
        catch (Exception ex)
        {
            Update(task.Id, "Errored", TaskState.Errored, CancellationToken.None);
            throw;
        }
    }

    private void Update(string message, TaskState state)
    {
        TaskGroupProgressChanged?.Invoke(this, new TaskGroupProgressEventArgs { Message = message, State = state });
    }

    private void Update(string id, string message, TaskState state)
    {
        SubTaskProgressChanged?.Invoke(this, new SubTaskProgressEventArgs { Id = id, Message = message, State = state });
    }

    private void Update(TaskState state, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        Update(state.ToString(), state);
    }

    private void Update(string id, string message, TaskState state, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        Update(id, message, state);
    }
}
