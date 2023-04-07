CREATE TABLE [Library].[ArtistImages]
(
	[ArtistId] INT NOT NULL PRIMARY KEY, 
    [Image] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_ArtistImages_ToArtists] FOREIGN KEY ([ArtistId]) REFERENCES [Library].[Artists]([Id])
)

GO
