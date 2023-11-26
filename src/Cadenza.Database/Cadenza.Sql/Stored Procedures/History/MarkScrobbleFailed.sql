CREATE PROCEDURE [History].[MarkScrobbleFailed]
	@ScrobbleId INT
AS
BEGIN

	UPDATE
		[History].[Scrobbles]
	SET
		[Scrobbled] = 0,
		[FailedAttempts] = [FailedAttempts] + 1
	WHERE
		[Id] = @ScrobbleId

END