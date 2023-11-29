CREATE PROCEDURE [History].[GetTopAlbums]
	@HistoryPeriod INT,
	@MaxItems INT
AS
BEGIN

	DECLARE @StartTime DATETIME = [History].[GetStartDate](@HistoryPeriod);

	WITH [Scrobbles] AS (
		SELECT 
			VTR.[AlbumId], COUNT(VSC.[ScrobbledAt]) [Count]
		FROM 
			[History].[vw_Scrobbles] VSC
		INNER JOIN
			[Library].[vw_TrackDetails] VTR ON VTR.[TrackId] = VSC.[TrackId]
		WHERE
			(@StartTime IS NULL OR VSC.[ScrobbledAt] >= @StartTime)
		GROUP BY
			VTR.[AlbumId]
	)
	SELECT TOP (@MaxItems)
		ALB.[Id],
		ALB.[Title],
		ART.[Name] [Artist],
		SCR.[Count] [Plays]
	FROM
		[Scrobbles] SCR
	INNER JOIN
		[Library].[Albums] ALB ON ALB.[Id] = SCR.[AlbumId]
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = ALB.[ArtistId]
	ORDER BY
		SCR.[Count] DESC

END