CREATE PROCEDURE [History].[GetRecentTracks]
	@Username NVARCHAR(100),
	@MaxItems INT
AS
BEGIN

	DECLARE @UserId INT

	SELECT 
		@UserId = [Id]
	FROM
		[Admin].[Users]
	WHERE
		[Username] = @Username;

	WITH [Scrobbles] AS (
		SELECT
			[TrackId],
			[Timestamp] [ScrobbledAt],
			1 [NowPlaying]
		FROM
			[History].[NowPlaying]
		WHERE
			[UserId] = @UserId
		AND
			DATEADD(SECOND, [SecondsRemaining], [Timestamp]) > GETDATE()

		UNION

		SELECT TOP (@MaxItems)
			TSC.[TrackId],
			SCR.[ScrobbledAt],
			0 [NowPlaying]
		FROM 
			[History].[TrackScrobbles] TSC
		INNER JOIN 
			[History].[Scrobbles] SCR ON SCR.[Id] = TSC.[ScrobbleId]
		ORDER BY
			SCR.[ScrobbledAt] DESC
	)
	SELECT TOP (@MaxItems)
		SCR.[TrackId] [Id],
		TRK.[Title] [Title],
		ART.[Name] [Artist],
		ALB.[Id] [AlbumId],
		ALB.[Title] [AlbumTitle],
		TRK.[IsLoved] [IsLoved],
		SCR.[ScrobbledAt],
		SCR.[NowPlaying]
	FROM 
		[Scrobbles] SCR
	INNER JOIN 
		[Library].[Tracks] TRK ON SCR.[TrackId] = TRK.[Id]
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = TRK.[ArtistId]
	INNER JOIN
		[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
	INNER JOIN
		[Library].[Albums] ALB ON ALB.[Id] = DSC.[AlbumId]
	ORDER BY
		SCR.[ScrobbledAt] DESC

END