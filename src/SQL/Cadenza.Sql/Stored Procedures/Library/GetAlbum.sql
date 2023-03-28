CREATE PROCEDURE [Library].[GetAlbum]
	@Id INT
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
		TAG.[TagList]
	FROM
		[Library].[Albums] ALB
	LEFT JOIN
		[Library].[vw_AlbumTags] TAG ON TAG.[AlbumId] = ALB.[Id]
	WHERE
		ALB.[Id] = @Id

END