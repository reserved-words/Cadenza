CREATE PROCEDURE [Library].[DeleteTrack]
	@SourceId INT,
	@IdFromSource NVARCHAR(500)
AS
BEGIN

	DELETE 
		TRK
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

END