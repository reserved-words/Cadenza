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

    public async Task RunTasks(List<StartupTask> subtasks)
    {
        var tasks = new List<Task>();

        foreach (var task in subtasks)
        {
            tasks.Add(PerformTask(task));
        }

        await Task.WhenAll(tasks).ContinueWith(parentTask =>
        {
            if (tasks.Any(t => t.IsFaulted))
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
            }
        });
    }

    private async Task PerformTask(StartupTask task)
    {
        try
        {
            if (task.CheckStep != null)
            {
                var isTaskNeeded = await task.CheckStep.Task();
                if (!isTaskNeeded)
                {
                    Update(task.Connector, "Completed", TaskState.Completed);
                    task.OnCompleted();
                    return;
                }
            }

            object result = null;

            foreach (var step in task.Steps)
            {
                Update(task.Connector, step.Caption, TaskState.Running);
                result = await step.Task(result);
            }

            Update(task.Connector, "Completed", TaskState.Completed);
            if (task.OnCompleted != null)
            {
                task.OnCompleted();
            }
        }
        catch (Exception ex)
        {
            Update(task.Connector, ex.Message, TaskState.Errored);
            if (task.OnError != null)
            {
                task.OnError(ex);
            }
            throw;
        }
    }

    private void Update(Connector connector, string message, TaskState state)
    {
        _dispatcher.Dispatch(new SubTaskProgressedAction(connector, message, state));
    }
}
