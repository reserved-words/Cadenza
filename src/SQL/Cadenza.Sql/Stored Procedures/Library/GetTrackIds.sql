CREATE PROCEDURE [Library].[GetTrackIds]
	@SourceId INT
AS
BEGIN

	SELECT 
		TRK.[IdFromSource]
	FROM
		[Library].[Tracks] TRK
	INNER JOIN 
		[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
	INNER JOIN 
		[Library].[Albums] ALB ON ALB.[Id] = DSC.[AlbumId]
		AND (@SourceId IS NULL OR ALB.[SourceId] = @SourceId)

END