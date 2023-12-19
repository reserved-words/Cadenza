CREATE PROCEDURE [Play].[GetAlbum]
	@Id INT
AS
BEGIN

	SELECT 
		ALB.[Title],
		ART.[Name] [ArtistName]
	FROM
		[Library].[Albums] ALB
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = ALB.[ArtistId]
	WHERE
		ALB.[Id] = @Id

END