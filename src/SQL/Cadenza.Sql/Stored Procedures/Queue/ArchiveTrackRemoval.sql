CREATE PROCEDURE [Queue].[ArchiveTrackRemoval]
	@RequestId INT
AS
BEGIN

	INSERT INTO
		[Queue].[ArchivedTrackRemovals] 
		(
			[RequestId],
			[TrackId],
			[SourceId],
			[TrackTitle],
			[TrackArtist],
			[AlbumTitle],
			[AlbumArtist],
			[DateQueued],
			[DateProcessed], 
			[DateRemoved],
			[DateErrored]
		)
	SELECT
		REM.[Id],
		REM.[TrackId],
		REM.[SourceId],
		TRK.[Title],
		ART.[Name],
		ALB.[Title],
		ALA.[Name],
		[DateQueued],
		[DateProcessed], 
		[DateRemoved],
		[DateErrored]
	FROM 
		[Queue].[TrackRemovals] REM
	INNER JOIN
		[Library].[Tracks] TRK ON TRK.[Id] = REM.[TrackId]
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = TRK.[ArtistId]
	INNER JOIN
		[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
	INNER JOIN
		[Library].[Albums] ALB ON ALB.[Id] = DSC.[AlbumId]
	INNER JOIN
		[Library].[Artists] ALA ON ALA.[Id] = ALB.[ArtistId]
	WHERE
		REM.[Id] = @RequestId

	DELETE 
		[Queue].[TrackRemovals]
	WHERE
		[Id] = @RequestId

END