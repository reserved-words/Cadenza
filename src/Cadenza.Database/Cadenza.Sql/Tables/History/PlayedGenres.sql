CREATE TABLE [History].[PlayedGenres]
(
	[PlayedItemId] INT NOT NULL PRIMARY KEY,
	[GenreId] NVARCHAR(100) NOT NULL,
	[GroupingId] INT NOT NULL DEFAULT 0,
	CONSTRAINT [FK_PlayedGenres_ToPlayedItems] FOREIGN KEY ([PlayedItemId]) REFERENCES [History].[PlayedItems]([Id]), 
    CONSTRAINT [FK_PlayedGenres_ToGroupings] FOREIGN KEY ([GroupingId]) REFERENCES [Admin].[Groupings]([Id])
)
