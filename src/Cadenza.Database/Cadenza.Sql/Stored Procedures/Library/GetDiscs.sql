CREATE PROCEDURE [Library].[GetDiscs]
	@SourceId INT = NULL
AS
BEGIN

	SELECT 
		DSC.[Id],
		DSC.[AlbumId],
		DSC.[Index],
		DSC.[TrackCount]
	FROM
		[Library].[Discs] DSC
	INNER JOIN
		[Library].[Albums] ALB ON ALB.[Id] = DSC.[AlbumId] 
		AND (@SourceId IS NULL OR ALB.[SourceId] = @SourceId)

END