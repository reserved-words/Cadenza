CREATE PROCEDURE [Queue].[MarkTrackRemovalDone]
	@Id INT
AS
BEGIN

	UPDATE
		[Queue].[TrackRemovalSync]
	SET
		[Synced] = 1,
		[LastAttempt] = GETDATE()
	WHERE
		[Id] = @Id

END