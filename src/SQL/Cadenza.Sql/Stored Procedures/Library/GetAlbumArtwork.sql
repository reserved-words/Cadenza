CREATE PROCEDURE [Library].[GetAlbumArtwork]
	@Id INT
AS
BEGIN

	SELECT 
		IMG.[MimeType],
		IMG.[Content]
	FROM
		[Library].[Albums] ALB
	LEFT JOIN
		[Library].[AlbumArtwork] IMG ON IMG.[AlbumId] = ALB.[Id]
	WHERE
		ALB.[Id] = @Id

END