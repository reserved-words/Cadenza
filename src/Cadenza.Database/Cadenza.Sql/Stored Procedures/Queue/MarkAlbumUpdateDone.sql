CREATE PROCEDURE [Queue].[MarkAlbumUpdateDone]
	@AlbumId INT
AS
BEGIN

	UPDATE
		[Queue].[AlbumSync]
	SET
		[LastSynced] = GETDATE(),
		[LastAttempt] = GETDATE()
	WHERE
		[AlbumId] = @AlbumId

END