CREATE PROCEDURE [LastFm].[QueueNowPlaying]
	@UserId INT
AS
BEGIN

	IF EXISTS (SELECT [UserId] FROM [LastFm].[NowPlaying] WHERE [UserId] = @UserId)
	BEGIN

		UPDATE 
			[LastFm].[NowPlaying]
		SET
			[Synced] = 0,
			[FailedAttempts] = 0,
			[LastAttempt] = NULL
		WHERE
			[UserId] = @UserId

	END
	ELSE
	BEGIN

		INSERT INTO [LastFm].[NowPlaying] (
			[UserId]
		)
		VALUES (
			@UserId
		)

	END

END