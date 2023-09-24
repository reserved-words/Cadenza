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

    public Task RunTasks(List<StartupTask> subtasks)
    {
        foreach (var task in subtasks)
        {
            _dispatcher.Dispatch(task.InitialAction);
        }

        return Task.CompletedTask;
    }

}
