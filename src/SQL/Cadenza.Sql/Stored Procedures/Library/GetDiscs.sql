CREATE PROCEDURE [Library].[GetDiscs]
	@SourceId INT = NULL
AS
BEGIN

	SELECT 
		D.[Id],
		D.[AlbumId],
		D.[Index],
		D.[TrackCount]
	FROM
		[Library].[Discs] D
	INNER JOIN
		[Library].[Albums] A ON A.[Id] = D.[AlbumId] 
		AND (@SourceId IS NULL OR A.[SourceId] = @SourceId)

END