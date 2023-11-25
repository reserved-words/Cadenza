CREATE TABLE [Queue].[AlbumUpdates]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[SourceId] INT NOT NULL,
	[AlbumId] INT NOT NULL,
	[PropertyId] INT NOT NULL,
	[OriginalValue] NVARCHAR(MAX),
	[UpdatedValue] NVARCHAR(MAX),
	[DateQueued] DATETIME NOT NULL DEFAULT GETDATE(),
	[DateProcessed] DATETIME NULL, 
	[DateRemoved] DATETIME NULL,
	[DateErrored] DATETIME NULL,
    CONSTRAINT [FK_AlbumUpdates_ToAlbums] FOREIGN KEY ([AlbumId]) REFERENCES [Library].[Albums]([Id]),
    CONSTRAINT [FK_AlbumUpdates_ToProperties] FOREIGN KEY ([PropertyId]) REFERENCES [Admin].[AlbumProperties]([Id]), 
    CONSTRAINT [FK_AlbumUpdates_ToSources] FOREIGN KEY ([SourceId]) REFERENCES [Admin].[Sources]([Id])
)

GO

CREATE INDEX [IX_AlbumUpdate_Properties] ON [Queue].[AlbumUpdates] ([AlbumId], [PropertyId])
GO
