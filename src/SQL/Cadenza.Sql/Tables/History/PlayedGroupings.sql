CREATE TABLE [History].[PlayedGroupings]
(
	[PlayedItemId] INT NOT NULL PRIMARY KEY,
	[GroupingId] INT NOT NULL,
	CONSTRAINT [FK_PlayedGroupings_ToPlayedItems] FOREIGN KEY ([PlayedItemId]) REFERENCES [History].[PlayedItems]([Id]),
    CONSTRAINT [FK_PlayedGroupings_ToGroupings] FOREIGN KEY ([GroupingId]) REFERENCES [Admin].[Groupings]([Id])
)
