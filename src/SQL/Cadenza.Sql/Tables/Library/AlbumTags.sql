CREATE TABLE [Library].[AlbumTags]
(
	[AlbumId] INT NOT NULL,
	[Tag] NVARCHAR(200), 
    CONSTRAINT [FK_AlbumTags_ToAlbum] FOREIGN KEY ([AlbumId]) REFERENCES [Library].[Albums]([Id])
)

GO

CREATE INDEX [IX_AlbumTags_Tag] ON [Library].[AlbumTags] ([Tag])
