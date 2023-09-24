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

    public async Task RunTasks(TaskGroup taskGroup)
    {
        try
        {
            if (taskGroup.PreTask != null)
            {
                Update(TaskState.Starting);
                await taskGroup.PreTask();
            }

            Update(TaskState.Running);

            var tasks = new List<Task>();

            foreach (var task in taskGroup.Tasks)
            {
                tasks.Add(PerformTask(task));
            }

            await Task.WhenAll(tasks).ContinueWith(parentTask =>
            {
                if (!tasks.Any(t => t.IsFaulted))
                {
                    Console.WriteLine("All tasks succeeded");
                    Update(TaskState.Completed);
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
                        Update(TaskState.Errored);
                    }
                    else
                    {
                        Update(TaskState.CompletedWithErrors);
                    }
                }
            });
        }
        catch (Exception)
        {
            Update(TaskState.Errored);
        }
    }

    private async Task PerformTask(SubTask task)
    {
        try
        {
            if (task.CheckStep != null)
            {
                var isTaskNeeded = await task.CheckStep.Task();
                if (!isTaskNeeded)
                {
                    Update(task.Id, "Completed", TaskState.Completed);
                    task.OnCompleted();
                    return;
                }
            }

            object result = null;

            foreach (var step in task.Steps)
            {
                Update(task.Id, step.Caption, TaskState.Running);
                result = await step.Task(result);
            }

            Update(task.Id, "Completed", TaskState.Completed);
            if (task.OnCompleted != null)
            {
                task.OnCompleted();
            }
        }
        catch (Exception ex)
        {
            Update(task.Id, ex.Message, TaskState.Errored);
            if (task.OnError != null)
            {
                task.OnError(ex);
            }
            throw;
        }
    }

    private void Update(TaskState state)
    {
        _dispatcher.Dispatch(new TaskGroupProgressedAction(state.GetDisplayName(), state));
    }

    private void Update(string id, string message, TaskState state)
    {
        _dispatcher.Dispatch(new SubTaskProgressedAction(id, message, state));
    }
}
