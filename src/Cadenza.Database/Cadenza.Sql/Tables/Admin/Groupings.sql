CREATE TABLE [Admin].[Groupings]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
)

GO

CREATE UNIQUE INDEX [UNQ_Groupings_Name] ON [Admin].[Groupings] ([Name])
GO
