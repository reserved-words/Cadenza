CREATE PROCEDURE [Library].[GetArtistAlbums]
	@ArtistId INT
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
		ALB.[ArtistId] = @ArtistId

END