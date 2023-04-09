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
		ART.[NameId] [ArtistNameId],
		ART.[Name] [ArtistName],
		TAG.[TagList]
	FROM
		[Library].[Albums] ALB
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = ALB.[ArtistId]
	LEFT JOIN
		[Library].[vw_AlbumTags] TAG ON TAG.[AlbumId] = ALB.[Id]
	WHERE
		@SourceId IS NULL 
		OR ALB.[SourceId] = @SourceId



END