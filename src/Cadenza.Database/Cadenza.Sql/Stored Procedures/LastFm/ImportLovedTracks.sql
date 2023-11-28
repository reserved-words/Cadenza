CREATE PROCEDURE [LastFm].[ImportLovedTracks]
AS
BEGIN

	UPDATE 
		T
	SET
		T.[IsLoved] = 1
	FROM
		[Library].[Tracks] T
	INNER JOIN 
		[Library].[Artists] A ON A.[Id] = T.[ArtistId]
	INNER JOIN
		[LastFm].[ImportedLovedTracks] LT ON LT.[Track] = T.[Title] AND LT.[Artist] = A.[Name] 
	WHERE 
		T.[IsLoved] = 0

END