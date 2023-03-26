CREATE PROCEDURE [Library].[GetTrackIds]
	@SourceId INT
AS
BEGIN

	SELECT 
		T.[IdFromSource]
	FROM
		[Library].[Tracks] T
	INNER JOIN 
		[Library].[Discs] D ON D.[Id] = T.[DiscId]
	INNER JOIN 
		[Library].[Albums] A ON A.[Id] = D.[AlbumId]
		AND (@SourceId IS NULL OR A.[SourceId] = @SourceId)

END