CREATE TABLE [Library].[Albums]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [SourceId] INT NOT NULL, 
    [ArtistId] INT NOT NULL, 
    [Title] NVARCHAR(500) NOT NULL,
    [ReleaseTypeId] INT NOT NULL,
    [Year] NCHAR(4) NOT NULL,
    [DiscCount] INT NOT NULL,
    [ArtworkUrl] NVARCHAR(500) NULL, 
    CONSTRAINT [FK_Albums_ToSources] FOREIGN KEY ([SourceId]) REFERENCES [Admin].[Sources]([Id]), 
    CONSTRAINT [FK_Albums_ToArtists] FOREIGN KEY ([ArtistId]) REFERENCES [Library].[Artists]([Id]), 
    CONSTRAINT [FK_Albums_ToReleaseTypes] FOREIGN KEY ([ReleaseTypeId]) REFERENCES [Admin].[ReleaseTypes]([Id])
)

GO

CREATE INDEX [UNQ_Albums] ON [Library].[Albums] ([ArtistId], [Title], [ReleaseTypeId])
GO


CREATE INDEX [IX_Albums_Source] ON [Library].[Albums] ([SourceId])
