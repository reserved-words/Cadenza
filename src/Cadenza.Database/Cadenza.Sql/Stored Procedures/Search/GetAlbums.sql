CREATE PROCEDURE [Search].[GetAlbums]
AS
BEGIN

	SELECT 
		ALB.[Id],
		ART.[Name] [ArtistName],
		ALB.[Title]
	FROM
		[Library].[Albums] ALB
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = ALB.[ArtistId]

END