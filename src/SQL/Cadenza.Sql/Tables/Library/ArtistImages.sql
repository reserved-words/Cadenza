CREATE TABLE [Library].[ArtistImages]
(
	[ArtistId] INT NOT NULL PRIMARY KEY, 
    [MimeType] NVARCHAR(30) NOT NULL, 
    [Content] VARBINARY(MAX) NOT NULL, 
    CONSTRAINT [FK_ArtistImages_ToArtists] FOREIGN KEY ([ArtistId]) REFERENCES [Library].[Artists]([Id])
)

GO
