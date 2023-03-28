CREATE TABLE [Queue].[AlbumUpdates]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[AlbumId] INT NOT NULL,
	[PropertyId] INT NOT NULL,
	[OriginalValue] NVARCHAR(MAX),
	[UpdatedValue] NVARCHAR(MAX),
	[DateQueued] DATETIME NOT NULL DEFAULT GETDATE(),
	[DateProcessed] DATETIME NULL, 
	[DateRemoved] DATETIME NULL,
	[DateErrored] DATETIME NULL,
    CONSTRAINT [FK_AlbumUpdates_ToProperties] FOREIGN KEY ([PropertyId]) REFERENCES [Admin].[AlbumProperties]([Id])
)

GO

CREATE INDEX [IX_AlbumUpdates] ON [Queue].[AlbumUpdates] ([AlbumId])
GO
