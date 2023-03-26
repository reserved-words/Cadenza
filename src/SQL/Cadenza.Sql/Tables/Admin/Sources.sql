CREATE TABLE [Admin].[Sources]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
)

GO

CREATE UNIQUE INDEX [UNQ_Sources_Name] ON [Admin].[Sources] ([Name])
GO
