CREATE TABLE [Queue].[TrackRemovalSync]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[SourceId] INT NOT NULL,
	[TrackIdFromSource] NVARCHAR(500) NOT NULL,
	[TrackTitle] NVARCHAR(200) NOT NULL,
	[TrackArtist] NVARCHAR(200) NOT NULL,
	[AlbumTitle] NVARCHAR(200) NOT NULL,
	[AlbumArtist] NVARCHAR(200) NOT NULL,
	[RemovedAt] DATETIME NOT NULL DEFAULT GETDATE(),
	[Synced] BIT NOT NULL DEFAULT 0, 
	[FailedAttempts] INT NOT NULL DEFAULT 0,
	[LastAttempt] DATETIME NULL,
    CONSTRAINT [FK_TrackRemovalSync_ToSources] FOREIGN KEY ([SourceId]) REFERENCES [Admin].[Sources]([Id])
)

GO
