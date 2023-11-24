CREATE PROCEDURE [Play].[GetArtistTrackIds]
	@Id INT,
	@LogRequest BIT
AS
BEGIN

	IF @LogRequest = 1
	BEGIN
		EXECUTE [Play].[LogArtistRequest] @Id
	END

	SELECT 
		TRK.[Id]
	FROM
		[Library].[Tracks] TRK
	WHERE
		TRK.[ArtistId] = @Id

END