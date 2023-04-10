CREATE TABLE [History].[PlayedTags]
(
	[PlayedItemId] INT NOT NULL PRIMARY KEY,
	[Tag] NVARCHAR(200) NOT NULL,
	CONSTRAINT [FK_PlayedTags_ToPlayedItems] FOREIGN KEY ([PlayedItemId]) REFERENCES [History].[PlayedItems]([Id])
)
