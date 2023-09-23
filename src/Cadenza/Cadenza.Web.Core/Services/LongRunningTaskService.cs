using Cadenza.Web.Common.Interfaces.Store;
using Fluxor;

namespace Cadenza.Web.Core.Services;

internal class LongRunningTaskService : ILongRunningTaskService
{
    private readonly IDispatcher _dispatcher;

    public LongRunningTaskService(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

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

            await Task.WhenAll(tasks).ContinueWith(parentTask =>
            {
                if (parentTask.IsCanceled)
                {
                    Console.WriteLine("Parent task cancelled");
                    Update(TaskState.Cancelling, CancellationToken.None);
                    Update(TaskState.Cancelled, CancellationToken.None);
                }
                else if (!tasks.Any(t => t.IsFaulted))
                {
                    Console.WriteLine("All tasks succeeded");
                    Update(TaskState.Completed, CancellationToken.None);
                }
                else
                {
                    Console.WriteLine("At least one task faulted");
                    foreach (var task in tasks.Where(t => t.IsFaulted))
                    {
                        foreach (var ex in task.Exception.InnerExceptions)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine(ex.StackTrace);
                        }
                    }

                    if (tasks.All(t => t.IsFaulted))
                    {
                        Update(TaskState.Errored, CancellationToken.None);
                    }
                    else
                    {
                        Update(TaskState.CompletedWithErrors, CancellationToken.None);
                    }
                }
            }, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            Update(TaskState.Cancelling, CancellationToken.None);
            // Add any cancellation tasks
            Update(TaskState.Cancelled, CancellationToken.None);
        }
        catch (Exception)
        {
            // Add any cancellation tasks
            Update(TaskState.Errored, CancellationToken.None);
        }
    }

    private async Task PerformTask(SubTask task, CancellationToken cancellationToken)
    {
        try
        {
            if (task.CheckStep != null)
            {
                var isTaskNeeded = await task.CheckStep.Task();
                if (!isTaskNeeded)
                {
                    Update(task.Id, "Completed", TaskState.Completed, CancellationToken.None);
                    task.OnCompleted();
                    return;
                }
            }

            object result = null;

            foreach (var step in task.Steps)
            {
                Update(task.Id, step.Caption, TaskState.Running, cancellationToken);
                result = await step.Task(result, cancellationToken);
            }

            Update(task.Id, "Completed", TaskState.Completed, CancellationToken.None);
            if (task.OnCompleted != null)
            {
                task.OnCompleted();
            }
        }
        catch (OperationCanceledException)
        {
            Update(task.Id, "Cancelled", TaskState.Cancelled, CancellationToken.None);
            throw;
        }
        catch (Exception ex)
        {
            Update(task.Id, ex.Message, TaskState.Errored, CancellationToken.None);
            if (task.OnError != null)
            {
                task.OnError(ex);
            }
            throw;
        }
    }

    private void Update(TaskState state, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _dispatcher.Dispatch(new TaskGroupProgressedAction(state.GetDisplayName(), state));
    }

    private void Update(string id, string message, TaskState state, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _dispatcher.Dispatch(new SubTaskProgressedAction(id, message, state));
    }
}
