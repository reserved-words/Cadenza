CREATE TABLE [Library].[AlbumArtwork]
(
	[AlbumId] INT NOT NULL PRIMARY KEY, 
    [Artwork] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_AlbumArtwork_ToAlbums] FOREIGN KEY ([AlbumId]) REFERENCES [Library].[Albums]([Id])
)

GO
