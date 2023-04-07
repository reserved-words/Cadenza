CREATE PROCEDURE [Library].[DeleteEmptyAlbums]
AS
BEGIN

	DELETE
		TAG
	FROM
		[Library].[AlbumTags] TAG
	INNER JOIN 
		[Library].[vw_EmptyAlbums] EMP ON EMP.[Id] = TAG.[AlbumId]
	
	DELETE
		IMG
	FROM
		[Library].[AlbumArtwork] IMG
	INNER JOIN 
		[Library].[vw_EmptyAlbums] EMP ON EMP.[Id] = IMG.[AlbumId]

	DELETE
		ALB
	FROM
		[Library].[Albums] ALB
	INNER JOIN 
		[Library].[vw_EmptyAlbums] EMP ON EMP.[Id] = ALB.[Id]

END