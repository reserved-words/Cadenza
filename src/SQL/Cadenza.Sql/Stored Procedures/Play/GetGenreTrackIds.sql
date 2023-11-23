CREATE PROCEDURE [Play].[GetGenreTrackIds]
	@Genre NVARCHAR(100)
AS
BEGIN

	SELECT 
		TRK.[Id]
	FROM
		[Library].[Tracks] TRK
	INNER JOIN 
		[Library].[Artists] ART ON ART.[Id] = TRK.[ArtistId]
	WHERE
		ART.[Genre] = @Genre

END