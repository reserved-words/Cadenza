CREATE PROCEDURE [LastFm].[GetNowPlayingUpdates]
AS
BEGIN
	
	SELECT
		LFM.[UserId],
		USR.[LastFmSessionKey] [SessionKey],
		NPL.[Timestamp],
		NPL.[SecondsRemaining],
		TRK.[Title] [Track],
		ART.[Name] [Artist],
		ALB.[Title] [Album],
		ALA.[Name] [AlbumArtist]
	FROM
		[LastFm].[NowPlaying] LFM
	INNER JOIN
		[History].[NowPlaying] NPL ON NPL.[UserId] = LFM.[UserId]
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