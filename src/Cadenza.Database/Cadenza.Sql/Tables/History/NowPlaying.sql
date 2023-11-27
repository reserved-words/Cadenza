CREATE TABLE [History].[NowPlaying]
(
	[UserId] INT NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES [Admin].[Users] ([Id]),
	[Timestamp] DATETIME NOT NULL,
	[TrackId] INT NULL FOREIGN KEY REFERENCES [Library].[Tracks] ([Id]),
	[SecondsRemaining] INT NOT NULL DEFAULT 0
)

GO