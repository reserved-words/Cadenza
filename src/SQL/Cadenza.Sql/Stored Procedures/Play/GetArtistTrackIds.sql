CREATE PROCEDURE [Play].[GetArtistTrackIds]
	@Id INT
AS
BEGIN

	SELECT 
		TRK.[Id]
	FROM
		[Library].[Tracks] TRK
	WHERE
		TRK.[ArtistId] = @Id

END