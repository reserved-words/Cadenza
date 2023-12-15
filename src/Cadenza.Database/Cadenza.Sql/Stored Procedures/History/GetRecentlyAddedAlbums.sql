CREATE PROCEDURE [History].[GetRecentlyAddedAlbums]
	@MaxItems INT
AS
BEGIN

	SELECT TOP (@MaxItems)
		ALB.[Id] [AlbumId],
		ALB.[Title] [AlbumTitle],
		ART.[Name] [ArtistName]
	FROM
		[Library].[Albums] ALB
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = ALB.[ArtistId]
	ORDER BY
		ALB.[Added] DESC,
		ALB.[Id] DESC

END

