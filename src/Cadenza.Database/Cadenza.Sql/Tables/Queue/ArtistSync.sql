CREATE TABLE [Queue].[ArtistSync]
(
	[ArtistId] INT NOT NULL,
	[LastUpdated] DATETIME NOT NULL,
	[LastSynced] DATETIME NULL,
	[FailedAttempts] INT NOT NULL DEFAULT 0,
	[LastAttempt] DATETIME NULL,
    CONSTRAINT [FK_ArtistSync_ToArtists] FOREIGN KEY ([ArtistId]) REFERENCES [Library].[Artists]([Id])
)

GO

CREATE UNIQUE INDEX [UNQ_ArtistSync_ArtistId] ON [Queue].[ArtistSync] ([ArtistId])
GO