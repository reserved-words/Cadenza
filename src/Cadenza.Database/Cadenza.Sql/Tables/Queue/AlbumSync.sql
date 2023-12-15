CREATE TABLE [Queue].[AlbumSync]
(
	[AlbumId] INT NOT NULL,
	[LastUpdated] DATETIME NOT NULL,
	[LastSynced] DATETIME NULL,
	[FailedAttempts] INT NOT NULL DEFAULT 0,
	[LastAttempt] DATETIME NULL,
    CONSTRAINT [FK_AlbumSync_ToAlbums] FOREIGN KEY ([AlbumId]) REFERENCES [Library].[Albums]([Id])
)

GO

CREATE UNIQUE INDEX [UNQ_AlbumSync_AlbumId] ON [Queue].[AlbumSync] ([AlbumId])
GO