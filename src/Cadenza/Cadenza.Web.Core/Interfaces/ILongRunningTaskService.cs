using Cadenza.Web.Common.Tasks;

namespace Cadenza.Web.Core.Interfaces;

public interface ILongRunningTaskService
{
    event TaskGroupProgressEventHandler TaskGroupProgressChanged;
    event SubTaskProgressEventHandler SubTaskProgressChanged;

    Task RunTasks(TaskGroup taskGroup, CancellationToken cancellationToken);
}
