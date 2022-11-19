using Cadenza.Web.Common.Tasks;
using Cadenza.Web.Core.Interfaces;
using Cadenza.Web.Core.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading;

namespace Cadenza.Web.Core.Tests;
public class LongRunningTaskServiceTests
{
	private readonly Mock<ILogger<LongRunningTaskService>> _mockLogger = new();
	private readonly Mock<ITaskProgressUpdater> _mockProgressUpdater = new();
	private readonly Mock<ITaskRunner> _mockTaskRunner = new();

	[SetUp]
	public void SetUp()
	{
		_mockLogger.Reset();
		_mockProgressUpdater.Reset();
	}

    [Test]
    public void RunTasks_()
    {
		var sut = new LongRunningTaskService(_mockProgressUpdater.Object, _mockLogger.Object, _mockTaskRunner.Object);
		var cancellationToken = CancellationToken.None;
		var taskGroup = new TaskGroup();


    }
}