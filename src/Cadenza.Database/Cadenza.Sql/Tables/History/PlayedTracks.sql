CREATE TABLE [History].[PlayedTracks]
(
	[PlayedItemId] INT NOT NULL PRIMARY KEY,
	[TrackId] INT NOT NULL,
	CONSTRAINT [FK_PlayedTracks_ToPlayedItems] FOREIGN KEY ([PlayedItemId]) REFERENCES [History].[PlayedItems]([Id]),
    CONSTRAINT [FK_PlayedTracks_ToTracks] FOREIGN KEY ([TrackId]) REFERENCES [Library].[Tracks]([Id])
)
