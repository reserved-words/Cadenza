CREATE TABLE [Admin].[ReleaseCategories]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
)

GO

CREATE UNIQUE INDEX [UNQ_ReleaseCategories_Name] ON [Admin].[ReleaseCategories] ([Name])
GO
