CREATE PROCEDURE [Play].[GetGenreTrackIds]
	@Genre NVARCHAR(100),
	@LogRequest BIT
AS
BEGIN

	IF @LogRequest = 1
	BEGIN
		EXECUTE [Play].[LogGenreRequest] @Genre
	END

	SELECT 
		TRK.[Id]
	FROM
		[Library].[Tracks] TRK
	INNER JOIN 
		[Library].[Artists] ART ON ART.[Id] = TRK.[ArtistId]
	WHERE
		ART.[Genre] = @Genre

END