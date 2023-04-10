CREATE TABLE [History].[PlayedGenres]
(
	[PlayedItemId] INT NOT NULL PRIMARY KEY,
	[GenreId] NVARCHAR(100) NOT NULL,
	CONSTRAINT [FK_PlayedGenres_ToPlayedItems] FOREIGN KEY ([PlayedItemId]) REFERENCES [History].[PlayedItems]([Id])
)
