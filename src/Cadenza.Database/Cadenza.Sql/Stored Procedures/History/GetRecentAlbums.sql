CREATE PROCEDURE [History].[GetRecentAlbums]
	@MaxItems INT
AS
BEGIN

	SELECT TOP (@MaxItems)
		PLA.[AlbumId],
		ALB.[Title] [AlbumTitle],
		ART.[Name] [ArtistName]
	FROM
		[History].[vw_PlayedAlbums] PLA
	INNER JOIN
		[Library].[Albums] ALB ON ALB.[Id] = PLA.[AlbumId]
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = ALB.[ArtistId]
	ORDER BY
		[PlayedAt] DESC

END

