using Cadenza.Web.Common.Interfaces.Store;
using Microsoft.Extensions.Logging;

namespace Cadenza.Web.Core.Services;

internal class LongRunningTaskService : ILongRunningTaskService
{
	private readonly ILogger<LongRunningTaskService> _logger;
	private readonly ITaskProgressUpdater _progressUpdater;
	private readonly ITaskRunner _taskRunner;

	public LongRunningTaskService(ITaskProgressUpdater progressUpdater, ILogger<LongRunningTaskService> logger, ITaskRunner taskRunner)
	{
		_progressUpdater = progressUpdater;
		_logger = logger;
		_taskRunner = taskRunner;
	}

	public async Task RunTasks(TaskGroup taskGroup, CancellationToken cancellationToken)
	{
		try
		{
			await _taskRunner.RunTaskGroup(taskGroup, cancellationToken);
		}
		catch (OperationCanceledException)
		{
			_progressUpdater.UpdateTask(TaskState.Cancelled, CancellationToken.None);
		}
		catch (Exception ex)
		{
			_logger.LogError("Long-running tasks errored", ex);
			_progressUpdater.UpdateTask(TaskState.Errored, CancellationToken.None);
		}
	}
}
