CREATE PROCEDURE [LastFm].[MarkNowPlayingFailed]
AS
BEGIN

	UPDATE
		[LastFm].[NowPlaying]
	SET
		[Synced] = 0,
		[FailedAttempts] = [FailedAttempts] + 1

END