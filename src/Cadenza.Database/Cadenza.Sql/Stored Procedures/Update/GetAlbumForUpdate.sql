CREATE PROCEDURE [Update].[GetAlbumForUpdate]
	@Id INT
AS
BEGIN

	SELECT 
		ALB.[ArtistId], 
		ALB.[Title],
		ALB.[ReleaseTypeId],
		ALB.[Year],
		ALB.[DiscCount],
		IMG.[MimeType] [ArtworkMimeType],
		IMG.[Content] [ArtworkContent],
		TAG.[TagList]
	FROM
		[Library].[Albums] ALB
	LEFT JOIN
		[Library].[vw_AlbumTags] TAG ON TAG.[AlbumId] = ALB.[Id]
	LEFT JOIN
		[Library].[AlbumArtwork] IMG ON IMG.[AlbumId] = ALB.[Id]
	WHERE
		ALB.[Id] = @Id

END