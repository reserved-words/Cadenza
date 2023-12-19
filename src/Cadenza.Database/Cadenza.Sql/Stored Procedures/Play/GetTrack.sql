CREATE PROCEDURE [Play].[GetTrack]
	@Id INT
AS
BEGIN

	SELECT 
		TRK.[Title],
		ART.[Name] [ArtistName]
	FROM
		[Library].[Tracks] TRK
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = TRK.[ArtistId]
	WHERE
		TRK.[Id] = @Id

END