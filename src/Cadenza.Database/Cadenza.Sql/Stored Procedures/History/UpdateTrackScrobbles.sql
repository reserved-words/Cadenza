CREATE PROCEDURE [History].[UpdateTrackScrobbles]
	@TrackId INT,
	@Title NVARCHAR(500)
AS
BEGIN

	UPDATE
		SCR
	SET
		SCR.[Track] = @Title
	FROM
		[History].[Scrobbles] SCR
	INNER JOIN
		[History].[TrackScrobbles] TSC ON TSC.[ScrobbleId] = SCR.[Id]
	WHERE
		TSC.[TrackId] = @TrackId

END