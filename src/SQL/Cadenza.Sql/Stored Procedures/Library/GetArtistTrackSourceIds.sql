CREATE PROCEDURE [Library].[GetArtistTrackSourceIds]
	@Id INT
AS
BEGIN

	SELECT 
		[IdFromSource]
	FROM
		[Library].[Tracks]
	WHERE
		[ArtistId] = @Id

END