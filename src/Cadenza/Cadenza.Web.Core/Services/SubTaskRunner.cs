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
			await TryRunSubTask(task, cancellationToken);
		}
		catch (OperationCanceledException)
		{
			_progressUpdater.UpdateSubTask(task.Id, "Cancelled", TaskState.Cancelled, CancellationToken.None);
			throw;
		}
		catch (Exception ex)
		{
			_progressUpdater.UpdateSubTask(task.Id, ex.Message, TaskState.Errored, CancellationToken.None);
			_logger.LogError("Subtask of long-running task errored", ex);
			if (task.OnError != null)
			{
				await task.OnError(ex);
			}
		}
	}

	private async Task TryRunSubTask(SubTask task, CancellationToken cancellationToken)
	{
		if (task.CheckStep != null)
		{
			var isTaskNeeded = await task.CheckStep.Task();
			if (!isTaskNeeded)
			{
				_progressUpdater.UpdateSubTask(task.Id, "Completed", TaskState.Completed, CancellationToken.None);
				await task.OnCompleted();
				return;
			}
		}

		object result = null;

		foreach (var step in task.Steps)
		{
			_progressUpdater.UpdateSubTask(task.Id, step.Caption, TaskState.Running, cancellationToken);
			result = await step.Task(result, cancellationToken);
		}

		_progressUpdater.UpdateSubTask(task.Id, "Completed", TaskState.Completed, CancellationToken.None);

		if (task.OnCompleted != null)
		{
			await task.OnCompleted();
		}
	}
}
