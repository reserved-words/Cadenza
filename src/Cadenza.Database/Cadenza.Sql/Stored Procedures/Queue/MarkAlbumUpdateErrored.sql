CREATE PROCEDURE [Queue].[MarkAlbumUpdateErrored]
	@AlbumId INT
AS
BEGIN

	UPDATE
		[Queue].[AlbumSync]
	SET
		[LastAttempt] = GETDATE(),
		[FailedAttempts] = [FailedAttempts] + 1
	WHERE
		[AlbumId] = @AlbumId

END