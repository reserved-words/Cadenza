CREATE PROCEDURE [LastFm].[MarkNowPlayingUpdated]
AS
BEGIN

	UPDATE
		[LastFm].[NowPlaying]
	SET
		[Synced] = 1,
		[LastAttempt] = GETDATE()

END