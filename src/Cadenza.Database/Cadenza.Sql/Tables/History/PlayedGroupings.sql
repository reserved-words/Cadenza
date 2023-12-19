CREATE TABLE [History].[PlayedGroupings]
(
	[PlayedItemId] INT NOT NULL PRIMARY KEY,
	[Grouping] NVARCHAR(50) NOT NULL DEFAULT 'None',
	CONSTRAINT [FK_PlayedGroupings_ToPlayedItems] FOREIGN KEY ([PlayedItemId]) REFERENCES [History].[PlayedItems]([Id])
)
