CREATE TABLE [Library].[Artists]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] NVARCHAR(200) NOT NULL, 
    [CompareName] NVARCHAR(200) NOT NULL, 
    [Grouping] NVARCHAR(50) NOT NULL DEFAULT 'None',
    [Genre] NVARCHAR(100) NOT NULL DEFAULT 'None', 
    [City] NVARCHAR(100) NULL, 
    [State] NVARCHAR(100) NULL, 
    [Country] NVARCHAR(100) NULL
)
GO

CREATE UNIQUE INDEX [UNQ_Artist_CompareName] ON [Library].[Artists] ([CompareName])
GO
CREATE UNIQUE INDEX [UNQ_Artist_Name] ON [Library].[Artists] ([Name])
GO


CREATE INDEX [IX_Artists_Genre] ON [Library].[Artists] ([Genre])

GO

CREATE INDEX [IX_Artists_Grouping] ON [Library].[Artists] ([Grouping])
