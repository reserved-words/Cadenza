CREATE TABLE [Library].[TrackTags]
(
	[TrackId] INT NOT NULL,
	[Tag] NVARCHAR(200), 
    CONSTRAINT [FK_TrackTags_ToTrack] FOREIGN KEY ([TrackId]) REFERENCES [Library].[Tracks]([Id])
)

GO

CREATE INDEX [IX_TrackTags_Tag] ON [Library].[TrackTags] ([Tag])
