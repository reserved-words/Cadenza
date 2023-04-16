CREATE PROCEDURE [Queue].[AddTrackRemoval]
	@TrackIdFromSource NVARCHAR(500)
AS
BEGIN

	UPDATE
		[Queue].[TrackRemovals]
	SET
		[DateRemoved] = GETDATE()
	WHERE
		[TrackIdFromSource] = @TrackIdFromSource
	AND
		[DateProcessed] IS NULL
	AND
		[DateRemoved] IS NULL

	INSERT INTO [Queue].[TrackRemovals] (
		[SourceId],
		[TrackIdFromSource],
		[TrackTitle],
		[TrackArtist],
		[AlbumTitle],
		[AlbumArtist]
	)
	SELECT
		ALB.[SourceId],
		TRK.[IdFromSource],
		TRK.[Title],
		ART.[Name],
		ALB.[Title],
		ALA.[Name]
	FROM 
		[Library].[Tracks] TRK
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = TRK.[ArtistId]
	INNER JOIN
		[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
	INNER JOIN
		[Library].[Albums] ALB ON ALB.[Id] = DSC.[AlbumId]
	INNER JOIN
		[Library].[Artists] ALA ON ALA.[Id] = ALB.[ArtistId]
	WHERE
		TRK.[IdFromSource] = @TrackIdFromSource

END