namespace Cadenza.Web.Common.Interfaces.Store;

public interface ILongRunningTaskService
{
    event TaskGroupProgressEventHandler TaskGroupProgressChanged;
    event SubTaskProgressEventHandler SubTaskProgressChanged;

    Task RunTasks(TaskGroup taskGroup, CancellationToken cancellationToken);
}
