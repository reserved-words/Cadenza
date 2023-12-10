CREATE PROCEDURE [Queue].[MarkTrackUpdateErrored]
	@TrackId INT
AS
BEGIN

	UPDATE
		[Queue].[TrackSync]
	SET
		[LastAttempt] = GETDATE(),
		[FailedAttempts] = [FailedAttempts] + 1
	WHERE
		[TrackId] = @TrackId

END