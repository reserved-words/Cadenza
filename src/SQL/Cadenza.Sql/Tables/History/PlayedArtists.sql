CREATE TABLE [History].[PlayedArtists]
(
	[PlayedItemId] INT NOT NULL PRIMARY KEY,
	[ArtistId] INT NOT NULL,
	CONSTRAINT [FK_PlayedArtists_ToPlayedItems] FOREIGN KEY ([PlayedItemId]) REFERENCES [History].[PlayedItems]([Id]),
    CONSTRAINT [FK_PlayedArtists_ToArtists] FOREIGN KEY ([ArtistId]) REFERENCES [Library].[Artists]([Id])
)
