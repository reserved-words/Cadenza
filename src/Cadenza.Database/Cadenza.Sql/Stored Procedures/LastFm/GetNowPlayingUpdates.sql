CREATE PROCEDURE [LastFm].[GetNowPlayingUpdates]
AS
BEGIN
	
	SELECT
		USR.[LastFmSessionKey] [SessionKey],
		NPL.[Timestamp],
		NPL.[SecondsRemaining],
		TRK.[Title] [Track],
		ART.[Name] [Artist],
		ALB.[Title] [Album],
		ALA.[Name] [AlbumArtist]
	FROM
		[Admin].[User] USR
	INNER JOIN
		[LastFm].[NowPlaying] LFM ON LFM.[Id] = 1
	INNER JOIN
		[History].[NowPlaying] NPL ON NPL.[Id] = 1
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
		LFM.[Synced] = 0
	AND
		LFM.[FailedAttempts] < 3
	AND
		USR.[LastFmSessionKey] IS NOT NULL
	AND
		NPL.[TrackId] IS NOT NULL
	AND
		NPL.[SecondsRemaining] > 0

END