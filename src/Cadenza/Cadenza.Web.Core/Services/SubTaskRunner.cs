using Microsoft.Extensions.Logging;

namespace Cadenza.Web.Core.Services;

internal class SubTaskRunner : ISubTaskRunner
{
	private readonly ILogger<SubTaskRunner> _logger;
	private readonly ITaskProgressUpdater _progressUpdater;

	public SubTaskRunner(ITaskProgressUpdater progressUpdater, ILogger<SubTaskRunner> logger)
	{
		_progressUpdater = progressUpdater;
		_logger = logger;
	}

	public async Task RunSubTask(SubTask task, CancellationToken cancellationToken)
	{
		try
		{
			await RunTask(task, cancellationToken);
			await HandleCompletion(task);
		}
		catch (OperationCanceledException)
		{
			HandleCancellation(task);
			throw;
		}
		catch (Exception ex)
		{
			await HandleError(task, ex);
		}
	}

	private void HandleCancellation(SubTask task)
	{
		_progressUpdater.UpdateSubTask(task.Id, "Cancelled", TaskState.Cancelled, CancellationToken.None);
	}

	private async Task HandleCompletion(SubTask task)
	{
		_progressUpdater.UpdateSubTask(task.Id, "Completed", TaskState.Completed, CancellationToken.None);

		if (task.OnCompleted != null)
		{
			await task.OnCompleted();
		}
	}

	private async Task HandleError(SubTask task, Exception ex)
	{
		_progressUpdater.UpdateSubTask(task.Id, ex.Message, TaskState.Errored, CancellationToken.None);
		_logger.LogError("Subtask of long-running task errored", ex);

		if (task.OnError != null)
		{
			await task.OnError(ex);
		}
	}

	private static async Task<bool> IsTaskNeeded(SubTask task)
	{
		if (task.CheckStep == null)
			return true;

		return await task.CheckStep.Task();
	}

	private async Task<object> RunStep(SubTask task, object result, TaskStep step, CancellationToken cancellationToken)
	{
		_progressUpdater.UpdateSubTask(task.Id, step.Caption, TaskState.Running, cancellationToken);
		return await step.Task(result, cancellationToken);
	}

	private async Task RunSteps(SubTask task, CancellationToken cancellationToken)
	{
		object result = null;

		foreach (var step in task.Steps)
		{
			result = await RunStep(task, result, step, cancellationToken);
		}
	}

	private async Task RunTask(SubTask task, CancellationToken cancellationToken)
	{
		var isTaskNeeded = await IsTaskNeeded(task);

		if (!isTaskNeeded)
			return;

		await RunSteps(task, cancellationToken);
	}
}
