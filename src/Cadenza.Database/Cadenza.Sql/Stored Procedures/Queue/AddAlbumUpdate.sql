CREATE PROCEDURE [Queue].[AddAlbumUpdate]
	@AlbumId INT
AS
BEGIN

	IF NOT EXISTS (SELECT [AlbumId] FROM [Queue].[AlbumSync] WHERE [AlbumId] = @AlbumId)
	BEGIN

		INSERT INTO [Queue].[AlbumSync] (
			[AlbumId],
			[LastUpdated]
		)
		VALUES (
			@AlbumId,
			GETDATE()
		)

	END
	ELSE
	BEGIN

		UPDATE 
			[Queue].[AlbumSync]
		SET
			[LastUpdated] = GETDATE(),
			[LastSynced] = NULL,
			[FailedAttempts] = 0,
			[LastAttempt] = NULL
		WHERE	
			[AlbumId] = @AlbumId

	END

END