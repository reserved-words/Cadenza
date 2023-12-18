CREATE PROCEDURE [Library].[GetFullAlbum]
	@Id INT
AS
BEGIN

	SELECT 
		ALB.[Id],
		ALB.[Title],
		ALB.[ReleaseTypeId],
		ALB.[Year],
		ALB.[DiscCount],
		TAG.[TagList],
		ALB.[ArtistId], 
		ART.[Name] [ArtistName],
		GRP.[Id] [ArtistGroupingId],
		GRP.[Name] [ArtistGroupingName],
		ART.[Genre] [ArtistGenre]
	FROM
		[Library].[Albums] ALB
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = ALB.[ArtistId]
	INNER JOIN
		[Admin].[Groupings] GRP ON GRP.[Id] = ART.[GroupingId]
	LEFT JOIN
		[Library].[vw_AlbumTags] TAG ON TAG.[AlbumId] = ALB.[Id]
	WHERE
		ALB.[Id] = @Id

END