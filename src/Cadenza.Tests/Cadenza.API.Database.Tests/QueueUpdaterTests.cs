using Cadenza.API.Database.Services.Updaters;
using Cadenza.Common.Domain.Model.Updates;
using NUnit.Framework;
using System.Collections.Generic;

namespace Cadenza.API.Database.Tests;

public class QueueUpdaterTests
{
    [Test]
    public void Queue_GivenEmptyQueue_AddsUpdate()
    {
		// Arrange
		var update = new EditedItem();
		var queue = new List<EditedItem>();
		var sut = new QueueUpdater();

		// Act
		sut.Queue(queue, update);

		// Assert
		Assert.That(queue, Is.Not.Null);
		Assert.That(queue, Has.Count.EqualTo(1));
		Assert.That(queue, Contains.Item(update));
	}
}