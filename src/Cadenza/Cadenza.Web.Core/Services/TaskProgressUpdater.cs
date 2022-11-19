namespace Cadenza.Web.Core.Services;

internal class TaskProgressUpdater : ITaskProgressUpdater
{
	private readonly IMessenger _messenger;

	public TaskProgressUpdater(IMessenger messenger)
	{
		_messenger = messenger;
	}

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

	public void UpdateSubTask(string id, string message, TaskState state, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		Update(id, message, state);
	}

	public void UpdateTask(TaskState state, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		Update(state.GetDisplayName(), state);
	}

#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

	private async Task Update(string message, TaskState state)
	{
		await _messenger.Send(this, new TaskGroupProgressEventArgs { Message = message, State = state });
	}

	private async Task Update(string id, string message, TaskState state)
	{
		await _messenger.Send(this, new SubTaskProgressEventArgs { Id = id, Message = message, State = state });
	}
}