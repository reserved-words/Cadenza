CREATE PROCEDURE [History].[MarkScrobbled]
	@ScrobbleId INT
AS
BEGIN

	UPDATE
		[History].[Scrobbles]
	SET
		[Scrobbled] = 1
	WHERE
		[Id] = @ScrobbleId

END