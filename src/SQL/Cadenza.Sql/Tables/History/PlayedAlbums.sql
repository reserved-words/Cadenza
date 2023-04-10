CREATE TABLE [History].[PlayedAlbums]
(
	[PlayedItemId] INT NOT NULL PRIMARY KEY,
	[AlbumId] INT NOT NULL,
	CONSTRAINT [FK_PlayedAlbums_ToPlayedItems] FOREIGN KEY ([PlayedItemId]) REFERENCES [History].[PlayedItems]([Id]),
    CONSTRAINT [FK_PlayedAlbums_ToAlbums] FOREIGN KEY ([AlbumId]) REFERENCES [Library].[Albums]([Id])
)
