CREATE PROCEDURE [History].[MarkNowPlayingFailed]
	@UserId INT
AS
BEGIN

	UPDATE
		[History].[NowPlaying]
	SET
		[Scrobbled] = 0,
		[FailedAttempts] = [FailedAttempts] + 1
	WHERE
		[UserId] = @UserId

END