CREATE TABLE [Queue].[ArchivedTrackRemovals]
(
	[RequestId] INT NOT NULL,
	[TrackId] INT NOT NULL,
	[SourceId] INT NOT NULL,
	[TrackTitle] NVARCHAR(200) NOT NULL,
	[TrackArtist] NVARCHAR(200) NOT NULL,
	[AlbumTitle] NVARCHAR(200) NOT NULL,
	[AlbumArtist] NVARCHAR(200) NOT NULL,
	[DateQueued] DATETIME NOT NULL,
	[DateProcessed] DATETIME NULL, 
	[DateRemoved] DATETIME NULL,
	[DateErrored] DATETIME NULL
)

GO
