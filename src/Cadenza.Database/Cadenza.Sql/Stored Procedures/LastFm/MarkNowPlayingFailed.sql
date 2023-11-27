CREATE PROCEDURE [LastFm].[MarkNowPlayingFailed]
	@UserId INT
AS
BEGIN

	UPDATE
		[LastFm].[NowPlaying]
	SET
		[Synced] = 0,
		[FailedAttempts] = [FailedAttempts] + 1
	WHERE
		[UserId] = @UserId

END