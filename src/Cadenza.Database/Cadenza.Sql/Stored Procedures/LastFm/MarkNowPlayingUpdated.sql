CREATE PROCEDURE [LastFm].[MarkNowPlayingUpdated]
	@UserId INT
AS
BEGIN

	UPDATE
		[LastFm].[NowPlaying]
	SET
		[Synced] = 1,
		[LastAttempt] = GETDATE()
	WHERE
		[UserId] = @UserId

END