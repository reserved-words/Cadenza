CREATE TABLE [Library].[Tracks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[IdFromSource] NVARCHAR(500) NOT NULL,
	[ArtistId] INT NOT NULL,
	[DiscId] INT NOT NULL,
	[TrackNo] INT NOT NULL,
	[Title] NVARCHAR(500) NOT NULL,
	[DurationSeconds] INT NOT NULL,
	[Year] NCHAR(4) NOT NULL,
	[Lyrics] NVARCHAR(MAX), 
	[IsLoved] BIT NOT NULL DEFAULT 0
    CONSTRAINT [FK_Tracks_ToArtists] FOREIGN KEY ([ArtistId]) REFERENCES [Library].[Artists]([Id]), 
    CONSTRAINT [FK_Tracks_ToDiscs] FOREIGN KEY ([DiscId]) REFERENCES [Library].[Discs]([Id])
)

GO

CREATE INDEX [UNQ_Tracks] ON [Library].[Tracks] ([IdFromSource])
GO

CREATE INDEX [UNQ_DiscTracks] ON [Library].[Tracks] ([DiscId],[TrackNo])
GO




CREATE INDEX [IX_Tracks_ArtistId] ON [Library].[Tracks] ([ArtistId])

GO

CREATE INDEX [IX_Tracks_DiscId] ON [Library].[Tracks] ([DiscId])
