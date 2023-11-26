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
		SCR.[Scrobbled] = 0
	AND
		USR.[LastFmSessionKey] IS NOT NULL

END