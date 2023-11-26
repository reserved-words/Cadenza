CREATE PROCEDURE [History].[MarkScrobbled]
	@ScrobbleId INT
AS
BEGIN

	UPDATE
		[History].[Scrobbles]
	SET
		[Scrobbled] = 1,
		[LastAttempt] = GETDATE()
	WHERE
		[Id] = @ScrobbleId

END