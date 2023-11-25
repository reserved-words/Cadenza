CREATE TABLE [Admin].[ArtistProperties]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
)

GO

CREATE INDEX [UNQ_ArtistProperties] ON [Admin].[ArtistProperties] ([Name])
