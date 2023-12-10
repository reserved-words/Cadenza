CREATE TABLE [Queue].[TrackSync]
(
	[TrackId] INT NOT NULL,
	[LastUpdated] DATETIME NOT NULL,
	[LastSynced] DATETIME NULL,
	[FailedAttempts] INT NOT NULL DEFAULT 0,
	[LastAttempt] DATETIME NULL,
    CONSTRAINT [FK_TrackSync_ToTracks] FOREIGN KEY ([TrackId]) REFERENCES [Library].[Tracks]([Id])
)

GO

CREATE UNIQUE INDEX [UNQ_TrackSync_TrackId] ON [Queue].[TrackSync] ([TrackId])
GO