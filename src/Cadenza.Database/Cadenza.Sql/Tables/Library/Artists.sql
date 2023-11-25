CREATE TABLE [Library].[Artists]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] NVARCHAR(200) NOT NULL, 
    [CompareName] NVARCHAR(200) NOT NULL, 
    [GroupingId] INT NOT NULL, 
    [Genre] NVARCHAR(100) NOT NULL, 
    [City] NVARCHAR(100) NULL, 
    [State] NVARCHAR(100) NULL, 
    [Country] NVARCHAR(100) NULL, 
    CONSTRAINT [FK_Artists_ToGroupings] FOREIGN KEY ([GroupingId]) REFERENCES [Admin].[Groupings]([Id])
)
GO

CREATE UNIQUE INDEX [UNQ_Artist_CompareName] ON [Library].[Artists] ([CompareName])
GO
CREATE UNIQUE INDEX [UNQ_Artist_Name] ON [Library].[Artists] ([Name])
GO


CREATE INDEX [IX_Artists_Genre] ON [Library].[Artists] ([Genre])

GO

CREATE INDEX [IX_Artists_GroupingId] ON [Library].[Artists] ([GroupingId])
