CREATE TABLE [Library].[Tracks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[IdFromSource] NVARCHAR(500),
	[ArtistId] INT NOT NULL,
	[DiscId] INT NOT NULL,
	[TrackNo] INT NOT NULL,
	[Title] NVARCHAR(500) NOT NULL,
	[DurationSeconds] INT NOT NULL,
	[Year] NCHAR(4) NOT NULL,
	[Lyrics] NVARCHAR(MAX), 
    CONSTRAINT [FK_Tracks_ToArtists] FOREIGN KEY ([ArtistId]) REFERENCES [Library].[Artists]([Id]), 
    CONSTRAINT [FK_Tracks_ToDiscs] FOREIGN KEY ([DiscId]) REFERENCES [Library].[Discs]([Id])
)

GO

CREATE INDEX [UNQ_Tracks] ON [Library].[Tracks] ([ArtistId],[DiscId],[TrackNo])
GO


