CREATE PROCEDURE [Library].[GetAlbum]
	@Id INT
AS
BEGIN

	SELECT 
		ALB.[Id],
		ALB.[ArtistId], 
		ART.[Name] [ArtistName],
		ALB.[Title],
		ALB.[ReleaseTypeId],
		ALB.[Year],
		ALB.[DiscCount],
		TAG.[TagList]
	FROM
		[Library].[Albums] ALB
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = ALB.[ArtistId]
	LEFT JOIN
		[Library].[vw_AlbumTags] TAG ON TAG.[AlbumId] = ALB.[Id]
	WHERE
		ALB.[Id] = @Id

END