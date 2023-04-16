CREATE TABLE [Queue].[TrackRemovals]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[SourceId] INT NOT NULL,
	[TrackIdFromSource] NVARCHAR(500) NOT NULL,
	[TrackTitle] NVARCHAR(200) NOT NULL,
	[TrackArtist] NVARCHAR(200) NOT NULL,
	[AlbumTitle] NVARCHAR(200) NOT NULL,
	[AlbumArtist] NVARCHAR(200) NOT NULL,
	[DateQueued] DATETIME NOT NULL DEFAULT GETDATE(),
	[DateProcessed] DATETIME NULL, 
	[DateRemoved] DATETIME NULL,
	[DateErrored] DATETIME NULL, 
    CONSTRAINT [FK_TrackRemovals_ToSources] FOREIGN KEY ([SourceId]) REFERENCES [Admin].[Sources]([Id])
)

GO
