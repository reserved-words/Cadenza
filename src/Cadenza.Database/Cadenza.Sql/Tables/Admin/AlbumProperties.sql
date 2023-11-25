CREATE TABLE [Admin].[AlbumProperties]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
)

GO

CREATE INDEX [UNQ_AlbumProperties] ON [Admin].[AlbumProperties] ([Name])
