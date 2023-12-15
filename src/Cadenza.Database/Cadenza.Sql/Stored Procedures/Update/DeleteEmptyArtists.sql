CREATE PROCEDURE [Update].[DeleteEmptyArtists]
AS
BEGIN

	DELETE
		SYN
	FROM
		[Queue].[ArtistSync] SYN
	INNER JOIN 
		[Library].[vw_EmptyArtists] EMP ON EMP.[Id] = SYN.[ArtistId]

	DELETE
		HST
	FROM
		[History].[PlayedArtists] HST
	INNER JOIN 
		[Library].[vw_EmptyArtists] EMP ON EMP.[Id] = HST.[ArtistId]

	DELETE
		TAG
	FROM
		[Library].[ArtistTags] TAG
	INNER JOIN 
		[Library].[vw_EmptyArtists] EMP ON EMP.[Id] = TAG.[ArtistId]
	
	DELETE
		IMG
	FROM
		[Library].[ArtistImages] IMG
	INNER JOIN 
		[Library].[vw_EmptyArtists] EMP ON EMP.[Id] = IMG.[ArtistId]

	DELETE
		ART
	FROM
		[Library].[Artists] ART
	INNER JOIN 
		[Library].[vw_EmptyArtists] EMP ON EMP.[Id] = ART.[Id]

END