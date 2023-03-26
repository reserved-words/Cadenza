CREATE TABLE [Library].[Artists]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [NameId] NVARCHAR(200) NOT NULL, 
    [Name] NVARCHAR(200) NOT NULL, 
    [GroupingId] INT NOT NULL, 
    [Genre] NVARCHAR(100) NOT NULL, 
    [City] NVARCHAR(100) NULL, 
    [State] NVARCHAR(100) NULL, 
    [Country] NVARCHAR(100) NULL, 
    [ImageUrl] NVARCHAR(500) NULL, 
    CONSTRAINT [FK_Artists_ToGroupings] FOREIGN KEY ([GroupingId]) REFERENCES [Admin].[Groupings]([Id])
)
GO

CREATE UNIQUE INDEX [UNQ_Artist_NameId] ON [Library].[Artists] ([NameId])
GO

CREATE UNIQUE INDEX [UNQ_Artist_Name] ON [Library].[Artists] ([Name])
GO
