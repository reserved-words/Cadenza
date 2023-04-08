CREATE PROCEDURE [Library].[DeleteTrack]
	@SourceId INT,
	@IdFromSource NVARCHAR(500)
AS
BEGIN

	DECLARE @Id INT

	SELECT 
		@Id = TRK.[Id]
	FROM
		[Library].[Tracks] TRK
	INNER JOIN
		[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
	INNER JOIN
		[Library].[Albums] ALB ON ALB.[Id] = DSC.[AlbumId]
	WHERE 
		ALB.[SourceId] = @SourceId
	AND
		TRK.[IdFromSource] = @IdFromSource
		
	DELETE
		[Queue].[TrackUpdates]
	WHERE
		[TrackId] = @Id

	DELETE
		[Library].[TrackTags]
	WHERE
		[TrackId] = @Id

	DELETE 
		[Library].[Tracks]
	WHERE 
		[Id] = @Id

END