CREATE PROCEDURE [Library].[GetAlbums]
	@SourceId INT = NULL
AS
BEGIN

	SELECT 
		ALB.[Id],
		ALB.[SourceId], 
		ALB.[ArtistId], 
		ALB.[Title],
		ALB.[ReleaseTypeId],
		ALB.[Year],
		ALB.[DiscCount],
		ALB.[ArtworkUrl],
		ART.[NameId] [ArtistNameId],
		ART.[Name] [ArtistName]
	FROM
		[Library].[Albums] ALB
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = ALB.[ArtistId]
	WHERE
		@SourceId IS NULL 
		OR [SourceId] = @SourceId

END