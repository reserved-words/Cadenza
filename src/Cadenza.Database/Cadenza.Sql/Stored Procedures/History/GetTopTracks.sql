CREATE PROCEDURE [History].[GetTopTracks]
	@HistoryPeriod INT,
	@MaxItems INT
AS
BEGIN

	DECLARE @StartTime DATETIME = [History].[GetStartDate](@HistoryPeriod);

	WITH [Scrobbles] AS (
		SELECT 
			VSC.[TrackId], COUNT(VSC.[ScrobbledAt]) [Count]
		FROM 
			[History].[vw_Scrobbles] VSC
		WHERE
			(@StartTime IS NULL OR VSC.[ScrobbledAt] >= @StartTime)
		GROUP BY
			VSC.[TrackId]
	)
	SELECT TOP (@MaxItems)
		TRK.[Id],
		TRK.[Title],
		ART.[Name] [Artist],
		SCR.[Count] [Plays]
	FROM
		[Scrobbles] SCR
	INNER JOIN
		[Library].[Tracks] TRK ON TRK.[Id] = SCR.[TrackId]
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = TRK.[ArtistId]
	ORDER BY
		SCR.[Count] DESC

END