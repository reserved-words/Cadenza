CREATE TABLE [LastFm].[LovedTracks]
(
	[UserId] INT NOT NULL,
	[TrackId] INT NOT NULL,
	[Synced] BIT NOT NULL DEFAULT 0,
	[FailedAttempts] INT NOT NULL DEFAULT 0,
	[LastAttempt] DATETIME NULL,
    CONSTRAINT [FK_LastFmLovedTracks_ToTracks] FOREIGN KEY ([TrackId]) REFERENCES [Library].[Tracks]([Id]),
    CONSTRAINT [FK_LastFmLovedTracks_ToUsers] FOREIGN KEY ([UserId]) REFERENCES [Admin].[Users]([Id]), 
    CONSTRAINT [PK_LovedTracks] PRIMARY KEY ([UserId], [TrackId])
)
