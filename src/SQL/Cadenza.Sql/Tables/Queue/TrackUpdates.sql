CREATE TABLE [Queue].[TrackUpdates]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name] NVARCHAR(200),
	[SourceId] INT NOT NULL,
	[TrackId] INT NOT NULL,
	[PropertyId] INT NOT NULL,
	[OriginalValue] NVARCHAR(MAX),
	[UpdatedValue] NVARCHAR(MAX),
	[DateQueued] DATETIME NOT NULL DEFAULT GETDATE(),
	[DateProcessed] DATETIME NULL, 
	[DateRemoved] DATETIME NULL,
	[DateErrored] DATETIME NULL,
    CONSTRAINT [FK_TrackUpdates_ToProperties] FOREIGN KEY ([PropertyId]) REFERENCES [Admin].[TrackProperties]([Id]), 
    CONSTRAINT [FK_TrackUpdates_ToSources] FOREIGN KEY ([SourceId]) REFERENCES [Admin].[Sources]([Id])
)

GO

CREATE INDEX [IX_TrackUpdates] ON [Queue].[TrackUpdates] ([TrackId])
GO

CREATE INDEX [IX_TrackUpdate_Properties] ON [Queue].[TrackUpdates] ([TrackId], [PropertyId])
GO
