using Cadenza.API.Database.Services.Updaters;
using Cadenza.Common.Domain.Enums;
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

	[Test]
	public void Queue_GivenQueuedItem_IsDifferentType_AddsUpdate()
	{
		// Arrange
		var queuedUpdate = new EditedItem { Type = LibraryItemType.Artist, Id = "abc" };
		var newUpdate = new EditedItem { Type = LibraryItemType.Track, Id = "abc" };
		var queue = new List<EditedItem> { queuedUpdate };
		var sut = new QueueUpdater();

		// Act
		sut.Queue(queue, newUpdate);

		// Assert
		Assert.That(queue, Is.Not.Null);
		Assert.That(queue, Has.Count.EqualTo(2));
		Assert.That(queue, Contains.Item(queuedUpdate));
		Assert.That(queue, Contains.Item(newUpdate));
	}

	[Test]
	public void Queue_GivenQueuedItem_HasDifferentId_AddsUpdate()
	{
		// Arrange
		var queuedUpdate = new EditedItem { Type = LibraryItemType.Artist, Id = "abc" };
		var newUpdate = new EditedItem { Type = LibraryItemType.Artist, Id = "def" };
		var queue = new List<EditedItem> { queuedUpdate };
		var sut = new QueueUpdater();

		// Act
		sut.Queue(queue, newUpdate);

		// Assert
		Assert.That(queue, Is.Not.Null);
		Assert.That(queue, Has.Count.EqualTo(2));
		Assert.That(queue, Contains.Item(queuedUpdate));
		Assert.That(queue, Contains.Item(newUpdate));
	}
}