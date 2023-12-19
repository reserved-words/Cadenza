CREATE TABLE [History].[PlayedGenres]
(
	[PlayedItemId] INT NOT NULL PRIMARY KEY,
	[Genre] NVARCHAR(100) NOT NULL DEFAULT 'None',
	[Grouping] NVARCHAR(50) NOT NULL DEFAULT 'None',
	CONSTRAINT [FK_PlayedGenres_ToPlayedItems] FOREIGN KEY ([PlayedItemId]) REFERENCES [History].[PlayedItems]([Id])
)
