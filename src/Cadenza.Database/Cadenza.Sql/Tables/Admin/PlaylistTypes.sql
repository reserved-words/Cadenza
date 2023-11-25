CREATE TABLE [Admin].[PlaylistTypes]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
)

GO

CREATE UNIQUE INDEX [UNQ_PlaylistTypes_Name] ON [Admin].[PlaylistTypes] ([Name])
GO
