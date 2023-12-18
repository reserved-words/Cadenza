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
		ALB.[Year]
	FROM
		[Library].[Albums] ALB
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = ALB.[ArtistId]
	WHERE
		ALB.[Id] = @Id

END