CREATE PROCEDURE [History].[GetNewScrobbles]
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
		[History].[Scrobbles] SCR
	INNER JOIN
		[Admin].[Users] USR ON USR.[Id] = SCR.[UserId]
	WHERE
		USR.[LastFmSessionKey] IS NOT NULL
	AND
		SCR.[Scrobbled] = 0
	AND
		SCR.[FailedAttempts] < 3 -- TODO: This is just to prevent the service retrying over and over - need to add something better
								-- that checks failed attempts as well as last attempt time etc, set max failed attempts from config etc

END