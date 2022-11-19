using Microsoft.Extensions.Logging;

namespace Cadenza.Web.Core.Services;

internal class TaskRunner : ITaskRunner
{
	private readonly ILogger<TaskRunner> _logger;
	private readonly ITaskProgressUpdater _progressUpdater;
	private readonly ISubTaskRunner _subTaskRunner;

	public TaskRunner(ITaskProgressUpdater progressUpdater, ILogger<TaskRunner> logger, ISubTaskRunner subTaskRunner)
	{
		_progressUpdater = progressUpdater;
		_logger = logger;
		_subTaskRunner = subTaskRunner;
	}

	public async Task RunTaskGroup(TaskGroup taskGroup, CancellationToken cancellationToken)
	{
		_progressUpdater.UpdateTask(TaskState.Starting, cancellationToken);

		await RunPreTask(taskGroup);

		_progressUpdater.UpdateTask(TaskState.Running, cancellationToken);

		RunSubTasks(taskGroup, cancellationToken);
	}

	private static async Task RunPreTask(TaskGroup taskGroup)
	{
		if (taskGroup.PreTask != null)
		{
			await taskGroup.PreTask();
		}
	}

	private void RunSubTasks(TaskGroup taskGroup, CancellationToken cancellationToken)
	{
		var tasks = taskGroup.Tasks.Select(t => _subTaskRunner.RunSubTask(t, cancellationToken)).ToList();

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
		Task.WhenAll(tasks).ContinueWith(t => HandleFinishedTasks(t, tasks), cancellationToken);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
	}

	private void HandleFinishedTasks(Task taskGroup, List<Task> subTasks)
	{
		if (taskGroup.IsFaulted)
		{
			if (subTasks.All(tsk => tsk.IsFaulted))
			{
				_progressUpdater.UpdateTask(TaskState.Errored, CancellationToken.None);
			}
			else
			{
				_progressUpdater.UpdateTask(TaskState.CompletedWithErrors, CancellationToken.None);
			}

			foreach (var ex in taskGroup.Exception.InnerExceptions)
			{
				_logger.LogError("Long-running task errored", ex);
			}

			throw taskGroup.Exception;
		}
		else if (taskGroup.IsCanceled)
		{
			_progressUpdater.UpdateTask(TaskState.Cancelled, CancellationToken.None);
		}
		else
		{
			_progressUpdater.UpdateTask(TaskState.Completed, CancellationToken.None);
		}
	}
}
