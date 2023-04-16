CREATE PROCEDURE [Queue].[AddTrackRemoval]
	@Id INT
AS
BEGIN

	DECLARE @IdFromSource NVARCHAR(500)

	SELECT 
		@IdFromSource = [IdFromSource]
	FROM
		[Library].[Tracks]
	WHERE
		[Id] = @Id

	UPDATE
		[Queue].[TrackRemovals]
	SET
		[DateRemoved] = GETDATE()
	WHERE
		[TrackIdFromSource] = @IdFromSource
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
		TRK.[Id] = @Id

END