namespace Cadenza.Web.Core.Interfaces;

internal interface ITaskRunner
{
	Task RunTaskGroup(TaskGroup taskGroup, CancellationToken cancellationToken);
}
