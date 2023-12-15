CREATE PROCEDURE [Queue].[AddArtistUpdate]
	@ArtistId INT
AS
BEGIN

	IF NOT EXISTS (SELECT [ArtistId] FROM [Queue].[ArtistSync] WHERE [ArtistId] = @ArtistId)
	BEGIN

		INSERT INTO [Queue].[ArtistSync] (
			[ArtistId],
			[LastUpdated]
		)
		VALUES (
			@ArtistId,
			GETDATE()
		)

	END
	ELSE
	BEGIN

		UPDATE 
			[Queue].[ArtistSync]
		SET
			[LastUpdated] = GETDATE(),
			[FailedAttempts] = 0,
			[LastAttempt] = NULL
		WHERE	
			[ArtistId] = @ArtistId

	END

END