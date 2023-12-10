CREATE PROCEDURE [Queue].[MarkArtistUpdateDone]
	@ArtistId INT
AS
BEGIN

	UPDATE
		[Queue].[ArtistSync]
	SET
		[LastSynced] = GETDATE(),
		[LastAttempt] = GETDATE()
	WHERE
		[ArtistId] = @ArtistId

END