using Cadenza.Web.Common.Tasks;
using Cadenza.Web.Core.Interfaces;
using Cadenza.Web.Core.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace Cadenza.Web.Core.Tests;

public class SubTaskRunnerTests
{
	private readonly Mock<ILogger<SubTaskRunner>> _mockLogger = new();
	private readonly Mock<ITaskProgressUpdater> _mockProgressUpdater = new();

	private bool _completionActionRan;
	private bool _step1Ran;
	private bool _step2Ran;
	private bool _step3Ran;

	[SetUp]
	public void SetUp()
	{
		_mockLogger.Reset();
		_mockProgressUpdater.Reset();

		_completionActionRan = false;
		_step1Ran = false;
		_step2Ran = false;
		_step3Ran = false;
	}

	private bool AllStepsRan => _step1Ran && _step2Ran && _step3Ran;
	private bool NoStepsRan => !_step1Ran && !_step2Ran && !_step3Ran;

	[TestCase(true)]
	[TestCase(false)]
	public async Task RunSubTask_GivenNotNeeded_DoesNotRunSteps_HandlesCompletion(bool hasCompletionAction)
	{
		// Arrange
		var cancellationToken = CancellationToken.None;
		var task = GetTestTask(true, false, hasCompletionAction);
		var sut = GetSubjectUnderTest();

		// Act
		await sut.RunSubTask(task, cancellationToken);

		// Assert
		VerifyProgressUpdated(task, "Completed", TaskState.Completed, cancellationToken);
		Assert.That(_completionActionRan, Is.EqualTo(hasCompletionAction));
		Assert.IsTrue(NoStepsRan);
	}

	private SubTaskRunner GetSubjectUnderTest()
	{
		return new SubTaskRunner(_mockProgressUpdater.Object, _mockLogger.Object);
	}

	private SubTask GetTestTask(bool hasCheckStep, bool isNeeded, bool hasCompletionAction)
	{
		var task = new SubTask { Id = "test" };

		if (hasCheckStep)
		{
			task.CheckStep = new TaskCheckStep
			{
				Task = () => Task.FromResult(isNeeded)
			};
		}

		if (hasCompletionAction)
		{
			task.OnCompleted = () =>
			{
				_completionActionRan = true;
				return Task.CompletedTask;
			};
		}

		task.AddStep("Step 1", () => { _step1Ran = true; return Task.CompletedTask; });
		task.AddStep("Step 2", () => { _step2Ran = true; return Task.CompletedTask; });
		task.AddStep("Step 3", () => { _step3Ran = true; return Task.CompletedTask; });

		return task;
	}

	private void VerifyProgressUpdated(SubTask task, string caption, TaskState state, CancellationToken cancellationToken)
	{
		_mockProgressUpdater.Verify(u => u.UpdateSubTask(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskState>(), It.IsAny<CancellationToken>()), Times.Once);
		_mockProgressUpdater.Verify(u => u.UpdateSubTask(task.Id, caption, state, cancellationToken), Times.Once);
	}
}