CREATE PROCEDURE [Update].[DeleteEmptyAlbums]
AS
BEGIN

	DELETE
		SYN
	FROM
		[Queue].[AlbumSync] SYN
	INNER JOIN 
		[Library].[vw_EmptyAlbums] EMP ON EMP.[Id] = SYN.[AlbumId]

	DELETE
		HST
	FROM
		[History].[PlayedAlbums] HST
	INNER JOIN 
		[Library].[vw_EmptyAlbums] EMP ON EMP.[Id] = HST.[AlbumId]

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