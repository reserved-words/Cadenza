CREATE PROCEDURE [Queue].[MarkTrackRemovalErrored]
	@Id INT
AS
BEGIN

	UPDATE
		[Queue].[TrackRemovalSync]
	SET
		[LastAttempt] = GETDATE(),
		[FailedAttempts] = [FailedAttempts] + 1
	WHERE
		[Id] = @Id

END