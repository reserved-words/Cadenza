CREATE PROCEDURE [Library].[DeleteEmptyArtists]
AS
BEGIN

	DELETE
		TAG
	FROM
		[Library].[ArtistTags] TAG
	INNER JOIN 
		[Library].[vw_EmptyArtists] EMP ON EMP.[Id] = TAG.[ArtistId]

	DELETE
		ART
	FROM
		[Library].[Artists] ART
	INNER JOIN 
		[Library].[vw_EmptyArtists] EMP ON EMP.[Id] = ART.[Id]

END