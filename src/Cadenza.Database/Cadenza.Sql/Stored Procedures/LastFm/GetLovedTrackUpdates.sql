CREATE PROCEDURE [LastFm].[GetLovedTrackUpdates]
AS
BEGIN
	
	SELECT
		LFM.[TrackId],
		USR.[LastFmSessionKey] [SessionKey],
		TRK.[Title] [Track],
		ART.[Name] [Artist],
		TRK.[IsLoved] [IsLoved]
	FROM
		[LastFm].[LovedTracks] LFM
	INNER JOIN
		[Admin].[Users] USR ON USR.[Id] = LFM.[UserId]
	INNER JOIN
		[Library].[Tracks] TRK ON TRK.[Id] = LFM.[TrackId]
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = TRK.[ArtistId]
	WHERE
		LFM.[Synced] = 0
	AND
		LFM.[FailedAttempts] < 3
	AND
		USR.[LastFmSessionKey] IS NOT NULL

END