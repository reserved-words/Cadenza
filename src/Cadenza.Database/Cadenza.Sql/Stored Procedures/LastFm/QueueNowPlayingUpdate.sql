CREATE PROCEDURE [LastFm].[QueueNowPlaying]
AS
BEGIN

	IF EXISTS (SELECT [Id] FROM [LastFm].[NowPlaying])
	BEGIN

		UPDATE 
			[LastFm].[NowPlaying]
		SET
			[Synced] = 0,
			[FailedAttempts] = 0,
			[LastAttempt] = NULL

	END
	ELSE
	BEGIN

		INSERT INTO [LastFm].[NowPlaying] (
			[Id]
		)
		VALUES (
			1
		)

	END

END