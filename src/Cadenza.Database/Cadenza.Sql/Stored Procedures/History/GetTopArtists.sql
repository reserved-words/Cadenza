CREATE PROCEDURE [History].[GetTopArtists]
	@HistoryPeriod INT,
	@MaxItems INT
AS
BEGIN

	DECLARE @StartTime DATETIME = [History].[GetStartDate](@HistoryPeriod);

	WITH [Scrobbles] AS (
		SELECT 
			VTR.[ArtistId], COUNT(VSC.[ScrobbledAt]) [Count]
		FROM 
			[History].[vw_Scrobbles] VSC
		INNER JOIN
			[Library].[vw_TrackDetails] VTR ON VTR.[TrackId] = VSC.[TrackId]
		WHERE
			(@StartTime IS NULL OR VSC.[ScrobbledAt] >= @StartTime)
		GROUP BY
			VTR.[ArtistId]
	)
	SELECT TOP (@MaxItems)
		ART.[Id],
		ART.[Name],
		SCR.[Count] [Plays]
	FROM
		[Scrobbles] SCR
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = SCR.[ArtistId]
	ORDER BY
		SCR.[Count] DESC

END