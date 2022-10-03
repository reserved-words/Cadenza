using Cadenza.Web.Common.Interfaces.Store;

namespace Cadenza.Web.Core.Services;

internal class LongRunningTaskService : ILongRunningTaskService
{
    private readonly IMessenger _messenger;

    public LongRunningTaskService(IMessenger messenger)
    {
        _messenger = messenger;
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

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.WhenAll(tasks).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    if (tasks.All(tsk => tsk.IsFaulted))
                    {
                        Update(TaskState.Errored, CancellationToken.None);
                    }
                    else
                    {
                        Update(TaskState.CompletedWithErrors, CancellationToken.None);
                    }

                    foreach (var ex in t.Exception.InnerExceptions)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                    }

                    throw t.Exception;
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
            }, cancellationToken);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
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
                    await task.OnCompleted();
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
                await task.OnCompleted();
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
                await task.OnError(ex);
            }
        }
    }

    private async Task Update(string message, TaskState state)
    {
        await _messenger.Send(this, new TaskGroupProgressEventArgs { Message = message, State = state });
    }

    private async Task Update(string id, string message, TaskState state)
    {
        await _messenger.Send(this, new SubTaskProgressEventArgs { Id = id, Message = message, State = state });
    }

    private void Update(TaskState state, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        Update(state.GetDisplayName(), state);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
    }

    private void Update(string id, string message, TaskState state, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        Update(id, message, state);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
    }
}
