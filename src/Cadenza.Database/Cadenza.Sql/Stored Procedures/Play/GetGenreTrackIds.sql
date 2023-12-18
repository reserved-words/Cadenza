CREATE PROCEDURE [Play].[GetGenreTrackIds]
	@Genre NVARCHAR(100),
	@GroupingId INT,
	@LogRequest BIT
AS
BEGIN

	IF @LogRequest = 1
	BEGIN
		EXECUTE [Play].[LogGenreRequest] @Genre, @GroupingId
	END

	SELECT 
		TRK.[Id]
	FROM
		[Library].[Tracks] TRK
	INNER JOIN 
		[Library].[Artists] ART ON ART.[Id] = TRK.[ArtistId]
	WHERE
		ART.[Genre] = @Genre
	AND
		ART.[GroupingId] = @GroupingId

END