CREATE TABLE [Admin].[TrackProperties]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
)

GO

CREATE INDEX [UNQ_TrackProperties] ON [Admin].[TrackProperties] ([Name])
