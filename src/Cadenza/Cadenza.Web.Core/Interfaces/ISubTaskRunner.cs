namespace Cadenza.Web.Core.Interfaces;

internal interface ISubTaskRunner
{
	Task RunSubTask(SubTask task, CancellationToken cancellationToken);
}