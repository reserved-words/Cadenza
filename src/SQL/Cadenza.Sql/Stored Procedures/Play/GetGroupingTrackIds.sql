CREATE PROCEDURE [Play].[GetGroupingTrackIds]
	@Id INT
AS
BEGIN

	SELECT 
		TRK.[Id]
	FROM
		[Library].[Tracks] TRK
	INNER JOIN 
		[Library].[Artists] ART ON ART.[Id] = TRK.[ArtistId]
	WHERE
		ART.[GroupingId] = @Id

END