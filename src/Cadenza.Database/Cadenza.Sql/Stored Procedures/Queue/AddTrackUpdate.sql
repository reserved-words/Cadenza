CREATE PROCEDURE [Queue].[AddTrackUpdate]
	@TrackId INT
AS
BEGIN

	IF NOT EXISTS (SELECT [TrackId] FROM [Queue].[TrackSync] WHERE [TrackId] = @TrackId)
	BEGIN

		INSERT INTO [Queue].[TrackSync] (
			[TrackId],
			[LastUpdated]
		)
		VALUES (
			@TrackId,
			GETDATE()
		)

	END
	ELSE
	BEGIN

		UPDATE 
			[Queue].[TrackSync]
		SET
			[LastUpdated] = GETDATE(),
			[FailedAttempts] = 0,
			[LastAttempt] = NULL
		WHERE	
			[TrackId] = @TrackId

	END

END