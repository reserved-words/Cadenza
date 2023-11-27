CREATE PROCEDURE [LastFm].[MarkScrobbleFailed]
	@ScrobbleId INT
AS
BEGIN

	UPDATE
		[LastFm].[Scrobbles]
	SET
		[Synced] = 0,
		[FailedAttempts] = [FailedAttempts] + 1
	WHERE
		[ScrobbleId] = @ScrobbleId

END