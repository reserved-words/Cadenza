CREATE TABLE [Queue].[ArtistUpdates]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[ArtistId] INT NOT NULL,
	[PropertyId] INT NOT NULL,
	[OriginalValue] NVARCHAR(MAX),
	[UpdatedValue] NVARCHAR(MAX),
	[DateQueued] DATETIME NOT NULL DEFAULT GETDATE(),
	[DateProcessed] DATETIME NULL, 
	[DateRemoved] DATETIME NULL,
	[DateErrored] DATETIME NULL,
    CONSTRAINT [FK_ArtistUpdates_ToProperties] FOREIGN KEY ([PropertyId]) REFERENCES [Admin].[ArtistProperties]([Id])
)

GO

CREATE INDEX [IX_ArtistUpdates] ON [Queue].[ArtistUpdates] ([ArtistId])
GO

CREATE INDEX [IX_ArtistUpdate_Properties] ON [Queue].[ArtistUpdates] ([ArtistId], [PropertyId])
GO
