CREATE PROCEDURE [Queue].[MarkTrackUpdateDone]
	@TrackId INT
AS
BEGIN

	UPDATE
		[Queue].[TrackSync]
	SET
		[LastSynced] = GETDATE(),
		[LastAttempt] = GETDATE()
	WHERE
		[TrackId] = @TrackId

END