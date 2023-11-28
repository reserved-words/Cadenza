CREATE TABLE [LastFm].[NowPlaying]
(
	[UserId] INT NOT NULL PRIMARY KEY,
	[Synced] BIT NOT NULL DEFAULT 0,
	[FailedAttempts] INT NOT NULL DEFAULT 0,
	[LastAttempt] DATETIME NULL,
    CONSTRAINT [FK_LastFmNowPlaying_ToUsers] FOREIGN KEY ([UserId]) REFERENCES [Admin].[Users]([Id])
)
