CREATE TABLE [Admin].[ReleaseTypes]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL,
	[ReleaseCategoryId] INT NOT NULL, 
    CONSTRAINT [FK_ReleaseTypes_ToReleaseCategories] FOREIGN KEY ([ReleaseCategoryId]) REFERENCES [Admin].[ReleaseCategories]([Id])
)

GO

CREATE UNIQUE INDEX [UNQ_ReleaseTypes_Name] ON [Admin].[ReleaseTypes] ([Name])
GO
