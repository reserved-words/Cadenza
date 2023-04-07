CREATE TABLE [Library].[AlbumArtwork]
(
	[AlbumId] INT NOT NULL PRIMARY KEY, 
    [MimeType] NVARCHAR(30) NOT NULL, 
    [Content] VARBINARY(MAX) NOT NULL, 
    CONSTRAINT [FK_AlbumArtwork_ToAlbums] FOREIGN KEY ([AlbumId]) REFERENCES [Library].[Albums]([Id])
)

GO
