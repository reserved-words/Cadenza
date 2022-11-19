namespace Cadenza.Web.Core.Interfaces;

internal interface ITaskProgressUpdater
{
	void UpdateSubTask(string id, string message, TaskState state, CancellationToken cancellationToken);
	void UpdateTask(TaskState state, CancellationToken cancellationToken);
}
