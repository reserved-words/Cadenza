CREATE TABLE [Library].[Discs]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[AlbumId] INT NOT NULL,
	[DiscNo] INT NOT NULL, 
	[TrackCount] INT NOT NULL,
    CONSTRAINT [FK_Discs_ToAlbums] FOREIGN KEY ([AlbumId]) REFERENCES [Library].[Albums]([Id])
)

GO

CREATE INDEX [UNQ_Discs] ON [Library].[Discs] ([AlbumId], [DiscNo])

GO

CREATE INDEX [IX_Discs_AlbumId] ON [Library].[Discs] ([AlbumId])
