CREATE PROCEDURE [History].[MarkNowPlayingUpdated]
	@UserId INT
AS
BEGIN

	UPDATE
		[History].[NowPlaying]
	SET
		[Scrobbled] = 1,
		[LastAttempt] = GETDATE()
	WHERE
		[UserId] = @UserId

END