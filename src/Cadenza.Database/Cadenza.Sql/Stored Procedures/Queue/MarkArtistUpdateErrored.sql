CREATE PROCEDURE [Queue].[MarkArtistUpdateErrored]
	@ArtistId INT
AS
BEGIN

	UPDATE
		[Queue].[ArtistSync]
	SET
		[LastAttempt] = GETDATE(),
		[FailedAttempts] = [FailedAttempts] + 1
	WHERE
		[ArtistId] = @ArtistId

END