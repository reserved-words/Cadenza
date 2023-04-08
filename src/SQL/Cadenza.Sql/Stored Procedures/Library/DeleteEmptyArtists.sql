﻿CREATE PROCEDURE [Library].[DeleteEmptyArtists]
AS
BEGIN

	DELETE
		UPD
	FROM
		[Queue].[ArtistUpdates] UPD
	INNER JOIN 
		[Library].[vw_EmptyArtists] EMP ON EMP.[Id] = UPD.[ArtistId]

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