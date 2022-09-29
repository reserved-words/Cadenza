namespace Cadenza.Web.Common.Interfaces;

public interface ILongRunningTaskService
{
    event TaskGroupProgressEventHandler TaskGroupProgressChanged;
    event SubTaskProgressEventHandler SubTaskProgressChanged;

    Task RunTasks(TaskGroup taskGroup, CancellationToken cancellationToken);
}
