CREATE TABLE [Library].[ArtistTags]
(
	[ArtistId] INT NOT NULL,
	[Tag] NVARCHAR(200), 
    CONSTRAINT [FK_ArtistTags_ToArtist] FOREIGN KEY ([ArtistId]) REFERENCES [Library].[Artists]([Id])
)
