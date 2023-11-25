CREATE PROCEDURE [Library].[GetAlbumsFeaturingArtist]
	@ArtistId INT
AS
BEGIN

	WITH [SelectedAlbums] AS (
		SELECT DISTINCT
			ALB.[Id]
		FROM
			[Library].[Tracks] TRK
		INNER JOIN 
			[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
		INNER JOIN
			[Library].[Albums] ALB ON ALB.[Id] = DSC.[AlbumId]
		WHERE
			TRK.[ArtistId] = @ArtistId
			AND ALB.[ArtistId] != @ArtistId
	) 
	SELECT 
		ALB.[Id],
		ALB.[ArtistId], 
		ART.[Name] [ArtistName],
		ALB.[Title],
		ALB.[ReleaseTypeId],
		ALB.[Year]
	FROM
		[SelectedAlbums] SAL
	INNER JOIN 
		[Library].[Albums] ALB ON SAL.[Id] = ALB.[Id]
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = ALB.[ArtistId]

END