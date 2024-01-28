CREATE PROCEDURE [LastFm].[GetNewScrobbles]
AS
BEGIN
	
	SELECT
		SCR.[Id],
		USR.[LastFmSessionKey] [SessionKey],
		SCR.[ScrobbledAt],
		SCR.[Track],
		SCR.[Artist],
		SCR.[Album],
		SCR.[AlbumArtist]
	FROM
		[LastFm].[Scrobbles] LFM
	INNER JOIN
		[History].[Scrobbles] SCR ON SCR.[Id] = LFM.[ScrobbleId]
	INNER JOIN
		[Admin].[User] USR ON USR.[Id] = 1
	WHERE
		LFM.[Synced] = 0
	AND
		LFM.[FailedAttempts] < 3
	AND
		USR.[LastFmSessionKey] IS NOT NULL

END