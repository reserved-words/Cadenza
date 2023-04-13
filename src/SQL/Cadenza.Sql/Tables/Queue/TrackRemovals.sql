CREATE TABLE [Queue].[TrackRemovals]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[SourceId] INT NOT NULL,
	[TrackId] INT NOT NULL,
	[DateQueued] DATETIME NOT NULL DEFAULT GETDATE(),
	[DateProcessed] DATETIME NULL, 
	[DateRemoved] DATETIME NULL,
	[DateErrored] DATETIME NULL,
	CONSTRAINT [FK_TrackRemovals_ToTracks] FOREIGN KEY ([TrackId]) REFERENCES [Library].[Tracks]([Id]), 
    CONSTRAINT [FK_TrackRemovals_ToSources] FOREIGN KEY ([SourceId]) REFERENCES [Admin].[Sources]([Id])
)

GO
