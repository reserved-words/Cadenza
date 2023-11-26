CREATE PROCEDURE [History].[GetNowPlayingUpdates]
AS
BEGIN
	
	SELECT
		NPL.[UserId],
		USR.[LastFmSessionKey] [SessionKey],
		NPL.[Timestamp],
		NPL.[SecondsRemaining],
		TRK.[Title] [Track],
		ART.[Name] [Artist],
		ALB.[Title] [Album],
		ALA.[Name] [AlbumArtist]
	FROM
		[History].[NowPlaying] NPL
	INNER JOIN
		[Admin].[Users] USR ON USR.[Id] = NPL.[UserId]
	INNER JOIN
		[Library].[Tracks] TRK ON TRK.[Id] = NPL.[TrackId]
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = TRK.[ArtistId]
	INNER JOIN
		[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
	INNER JOIN
		[Library].[Albums] ALB ON ALB.[Id] = DSC.[AlbumId]
	INNER JOIN
		[Library].[Artists] ALA ON ALA.[Id] = ALB.[ArtistId]
	WHERE
		USR.[LastFmSessionKey] IS NOT NULL
	AND
		NPL.[TrackId] IS NOT NULL
	AND
		NPL.[Scrobbled] = 0
	AND
		NPL.[SecondsRemaining] > 0
	AND
		NPL.[FailedAttempts] < 3 -- TODO: This is just to prevent the service retrying over and over - need to add something better
								-- that checks failed attempts as well as last attempt time etc, set max failed attempts from config etc

END