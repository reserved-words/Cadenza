CREATE TABLE [LastFm].[Scrobbles]
(
	[ScrobbleId] INT NOT NULL PRIMARY KEY,
	[Synced] BIT NOT NULL DEFAULT 0,
	[FailedAttempts] INT NOT NULL DEFAULT 0,
	[LastAttempt] DATETIME NULL,
    CONSTRAINT [FK_LastFmScrobbles_ToScrobbles] FOREIGN KEY ([ScrobbleId]) REFERENCES [History].[Scrobbles]([Id])
)
