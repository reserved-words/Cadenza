CREATE TABLE [History].[NowPlaying]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Timestamp] DATETIME NOT NULL,
	[TrackId] INT NULL FOREIGN KEY REFERENCES [Library].[Tracks] ([Id]),
	[SecondsRemaining] INT NOT NULL DEFAULT 0
)

GO