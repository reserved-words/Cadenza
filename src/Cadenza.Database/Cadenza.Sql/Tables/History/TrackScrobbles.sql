CREATE TABLE [History].[TrackScrobbles]
(
	[ScrobbleId] INT NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES [History].[Scrobbles] ([Id]),
	[TrackId] INT NOT NULL FOREIGN KEY REFERENCES [Library].[Tracks] ([Id])
)

GO

CREATE INDEX [IX_TrackScrobbles_TrackId] ON [History].[TrackScrobbles] ([TrackId])

GO
