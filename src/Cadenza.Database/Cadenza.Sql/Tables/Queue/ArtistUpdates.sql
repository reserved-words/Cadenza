﻿CREATE TABLE [Queue].[ArtistUpdates]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[SourceId] INT NOT NULL,
	[ArtistId] INT NOT NULL,
	[PropertyId] INT NOT NULL,
	[OriginalValue] NVARCHAR(MAX),
	[UpdatedValue] NVARCHAR(MAX),
	[DateQueued] DATETIME NOT NULL DEFAULT GETDATE(),
	[DateProcessed] DATETIME NULL, 
	[DateRemoved] DATETIME NULL,
	[DateErrored] DATETIME NULL,
    CONSTRAINT [FK_ArtistUpdates_ToArtists] FOREIGN KEY ([ArtistId]) REFERENCES [Library].[Artists]([Id]),
    CONSTRAINT [FK_ArtistUpdates_ToProperties] FOREIGN KEY ([PropertyId]) REFERENCES [Admin].[ArtistProperties]([Id]), 
    CONSTRAINT [FK_ArtistUpdates_ToSources] FOREIGN KEY ([SourceId]) REFERENCES [Admin].[Sources]([Id])
)

GO

CREATE INDEX [IX_ArtistUpdate_Properties] ON [Queue].[ArtistUpdates] ([ArtistId], [PropertyId])
GO