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
		var newUpdate = new EditedItem();
		var queue = new List<EditedItem>();
		var sut = new QueueUpdater();

		// Act
		sut.Queue(queue, newUpdate);

		// Assert
		Assert.That(queue, Is.Not.Null);
		Assert.That(queue, Has.Count.EqualTo(1));
		Assert.That(queue, Contains.Item(newUpdate));
	}

	[Test]
	public void Queue_GivenQueuedItem_DifferentItemType_AddsUpdate()
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
	public void Queue_GivenQueuedItem_DifferentItemId_AddsUpdate()
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

	[Test]
	public void Queue_GivenQueuedItem_SameItem_DifferentProperty_AddsProperty()
	{
		// Arrange
		var queuedPropertyUpdate = new EditedProperty { Property = ItemProperty.Country };
		var queuedItemUpdate = new EditedItem { Type = LibraryItemType.Artist, Id = "abc" };
		queuedItemUpdate.Properties.Add(queuedPropertyUpdate);

		var newPropertyUpdate = new EditedProperty { Property = ItemProperty.City };
		var newItemUpdate = new EditedItem { Type = LibraryItemType.Artist, Id = "abc" };
		newItemUpdate.Properties.Add(newPropertyUpdate);
		
		var queue = new List<EditedItem> { queuedItemUpdate };
		var sut = new QueueUpdater();

		// Act
		sut.Queue(queue, newItemUpdate);

		// Assert
		Assert.That(queue, Is.Not.Null);
		Assert.That(queue, Has.Count.EqualTo(1));
		Assert.That(queue, Contains.Item(queuedItemUpdate));
		Assert.That(queuedItemUpdate.Properties, Has.Count.EqualTo(2));
		Assert.That(queuedItemUpdate.Properties, Contains.Item(queuedPropertyUpdate));
		Assert.That(queuedItemUpdate.Properties, Contains.Item(newPropertyUpdate));
	}

	[Test]
	public void Queue_GivenQueuedItem_SameItem_SameProperty_UpdatesProperty()
	{
		// Arrange
		var queuedPropertyUpdate = new EditedProperty { Property = ItemProperty.Country, OriginalValue = "1", UpdatedValue = "2" };
		var queuedItemUpdate = new EditedItem { Type = LibraryItemType.Artist, Id = "abc" };
		queuedItemUpdate.Properties.Add(queuedPropertyUpdate);

		var newPropertyUpdate = new EditedProperty { Property = ItemProperty.Country, OriginalValue = "2", UpdatedValue = "3" };
		var newItemUpdate = new EditedItem { Type = LibraryItemType.Artist, Id = "abc" };
		newItemUpdate.Properties.Add(newPropertyUpdate);

		var queue = new List<EditedItem> { queuedItemUpdate };
		var sut = new QueueUpdater();

		// Act
		sut.Queue(queue, newItemUpdate);

		// Assert
		Assert.That(queue, Is.Not.Null);
		Assert.That(queue, Has.Count.EqualTo(1));
		Assert.That(queue, Contains.Item(queuedItemUpdate));
		Assert.That(queuedItemUpdate.Properties, Has.Count.EqualTo(1));
		Assert.That(queuedItemUpdate.Properties, Contains.Item(queuedPropertyUpdate));
		Assert.That(queuedPropertyUpdate.OriginalValue, Is.EqualTo("1"));
		Assert.That(queuedPropertyUpdate.UpdatedValue, Is.EqualTo("3"));
	}

	[Test]
	public void Dequeue_GivenEmptyQueue_DoesNothing()
	{
		// Arrange
		var update = new EditedItem();
		var queue = new List<EditedItem>();
		var sut = new QueueUpdater();

		// Act
		sut.Dequeue(queue, update);

		// Assert
		Assert.That(queue, Is.Not.Null);
		Assert.That(queue, Is.Empty);
	}

	[Test]
	public void Dequeue_GivenQueuedItem_DifferentItemType_DoesNothing()
	{
		// Arrange
		var queuedUpdate = new EditedItem { Type = LibraryItemType.Artist, Id = "abc" };
		var removedUpdate = new EditedItem { Type = LibraryItemType.Track, Id = "abc" };
		var queue = new List<EditedItem> { queuedUpdate };
		var sut = new QueueUpdater();

		// Act
		sut.Dequeue(queue, removedUpdate);

		// Assert
		Assert.That(queue, Is.Not.Null);
		Assert.That(queue, Has.Count.EqualTo(1));
		Assert.That(queue, Contains.Item(queuedUpdate));
	}

	[Test]
	public void Dequeue_GivenQueuedItem_DifferentItemId_DoesNothing()
	{
		// Arrange
		var queuedUpdate = new EditedItem { Type = LibraryItemType.Artist, Id = "abc" };
		var removedUpdate = new EditedItem { Type = LibraryItemType.Artist, Id = "def" };
		var queue = new List<EditedItem> { queuedUpdate };
		var sut = new QueueUpdater();

		// Act
		sut.Dequeue(queue, removedUpdate);

		// Assert
		Assert.That(queue, Is.Not.Null);
		Assert.That(queue, Has.Count.EqualTo(1));
		Assert.That(queue, Contains.Item(queuedUpdate));
	}

	[Test]
	public void Dequeue_GivenQueuedItem_SameItem_DifferentProperty_DoesNothing()
	{
		// Arrange
		var queuedPropertyUpdate = new EditedProperty { Property = ItemProperty.Country };
		var queuedItemUpdate = new EditedItem { Type = LibraryItemType.Artist, Id = "abc" };
		queuedItemUpdate.Properties.Add(queuedPropertyUpdate);

		var removedPropertyUpdate = new EditedProperty { Property = ItemProperty.City };
		var removedItemUpdate = new EditedItem { Type = LibraryItemType.Artist, Id = "abc" };
		removedItemUpdate.Properties.Add(removedPropertyUpdate);

		var queue = new List<EditedItem> { queuedItemUpdate };
		var sut = new QueueUpdater();

		// Act
		sut.Dequeue(queue, removedItemUpdate);

		// Assert
		Assert.That(queue, Is.Not.Null);
		Assert.That(queue, Has.Count.EqualTo(1));
		Assert.That(queue, Contains.Item(queuedItemUpdate));
		Assert.That(queuedItemUpdate.Properties, Has.Count.EqualTo(1));
		Assert.That(queuedItemUpdate.Properties, Contains.Item(queuedPropertyUpdate));
	}

	[Test]
	public void Dequeue_GivenQueuedItem_SameItem_SameProperty_DifferentValue_DoesNothing()
	{
		// Arrange
		var queuedPropertyUpdate = new EditedProperty { Property = ItemProperty.Country, UpdatedValue = "QueuedValue" };
		var queuedItemUpdate = new EditedItem { Type = LibraryItemType.Artist, Id = "abc" };
		queuedItemUpdate.Properties.Add(queuedPropertyUpdate);

		var removedPropertyUpdate = new EditedProperty { Property = ItemProperty.City, UpdatedValue = "RemovedValue" };
		var removedItemUpdate = new EditedItem { Type = LibraryItemType.Artist, Id = "abc" };
		removedItemUpdate.Properties.Add(removedPropertyUpdate);

		var queue = new List<EditedItem> { queuedItemUpdate };
		var sut = new QueueUpdater();

		// Act
		sut.Dequeue(queue, removedItemUpdate);

		// Assert
		Assert.That(queue, Is.Not.Null);
		Assert.That(queue, Has.Count.EqualTo(1));
		Assert.That(queue, Contains.Item(queuedItemUpdate));
		Assert.That(queuedItemUpdate.Properties, Has.Count.EqualTo(1));
		Assert.That(queuedItemUpdate.Properties, Contains.Item(queuedPropertyUpdate));
		Assert.That(queuedPropertyUpdate.UpdatedValue, Is.EqualTo("QueuedValue"));
	}

	[Test]
	public void Dequeue_GivenQueuedItem_SameItem_SameProperty_SameValue_NoOtherPropertyOrItemUpdates_RemovesItemUpdate()
	{
		// Arrange
		var queuedPropertyUpdate = new EditedProperty { Property = ItemProperty.Country, OriginalValue = "1", UpdatedValue = "2" };
		var queuedItemUpdate = new EditedItem { Type = LibraryItemType.Artist, Id = "abc" };
		queuedItemUpdate.Properties.Add(queuedPropertyUpdate);

		var newPropertyUpdate = new EditedProperty { Property = ItemProperty.Country, OriginalValue = "2", UpdatedValue = "2" };
		var newItemUpdate = new EditedItem { Type = LibraryItemType.Artist, Id = "abc" };
		newItemUpdate.Properties.Add(newPropertyUpdate);

		var queue = new List<EditedItem> { queuedItemUpdate };
		var sut = new QueueUpdater();

		// Act
		sut.Dequeue(queue, newItemUpdate);

		// Assert
		Assert.That(queue, Is.Not.Null);
		Assert.That(queue, Is.Empty);
	}

	[Test]
	public void Dequeue_GivenQueuedItem_SameItem_SameProperty_SameValue_OtherPropertyUpdatesQueued_RemovesPropertyUpdate()
	{
		// Arrange
		var matchingPropertyUpdate = new EditedProperty { Property = ItemProperty.Country, OriginalValue = "1", UpdatedValue = "2" };
		var otherPropertyUpdate = new EditedProperty { Property = ItemProperty.City, OriginalValue = "1", UpdatedValue = "2" };

		var queuedItemUpdate = new EditedItem { Type = LibraryItemType.Artist, Id = "abc" };
		queuedItemUpdate.Properties.Add(matchingPropertyUpdate);
		queuedItemUpdate.Properties.Add(otherPropertyUpdate);

		var newPropertyUpdate = new EditedProperty { Property = ItemProperty.Country, OriginalValue = "2", UpdatedValue = "2" };
		var newItemUpdate = new EditedItem { Type = LibraryItemType.Artist, Id = "abc" };
		newItemUpdate.Properties.Add(newPropertyUpdate);

		var queue = new List<EditedItem> { queuedItemUpdate };
		var sut = new QueueUpdater();

		// Act
		sut.Dequeue(queue, newItemUpdate);

		// Assert
		Assert.That(queue, Is.Not.Null);
		Assert.That(queue, Has.Count.EqualTo(1));
		Assert.That(queue, Contains.Item(queuedItemUpdate));
		Assert.That(queuedItemUpdate.Properties, Has.Count.EqualTo(1));
		Assert.That(queuedItemUpdate.Properties, Contains.Item(otherPropertyUpdate));
	}
}