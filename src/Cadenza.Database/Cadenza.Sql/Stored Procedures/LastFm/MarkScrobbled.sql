CREATE PROCEDURE [LastFm].[MarkScrobbled]
	@ScrobbleId INT
AS
BEGIN

	UPDATE
		[LastFm].[Scrobbles]
	SET
		[Synced] = 1,
		[LastAttempt] = GETDATE()
	WHERE
		[ScrobbleId] = @ScrobbleId

END