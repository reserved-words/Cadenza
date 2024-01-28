CREATE TABLE [LastFm].[LovedTracks]
(
	[TrackId] INT NOT NULL PRIMARY KEY,
	[Synced] BIT NOT NULL DEFAULT 0,
	[FailedAttempts] INT NOT NULL DEFAULT 0,
	[LastAttempt] DATETIME NULL,
    CONSTRAINT [FK_LastFmLovedTracks_ToTracks] FOREIGN KEY ([TrackId]) REFERENCES [Library].[Tracks]([Id])
)
