CREATE PROCEDURE [History].[UpdateAlbumScrobbles]
	@AlbumId INT,
	@Title NVARCHAR(500)
AS
BEGIN

	UPDATE
		SCR
	SET
		SCR.[Album] = @Title
	FROM
		[History].[Scrobbles] SCR
	INNER JOIN
		[History].[TrackScrobbles] TSC ON TSC.[ScrobbleId] = SCR.[Id]
	INNER JOIN
		[Library].[Tracks] TRK ON TRK.[Id] = TSC.[TrackId]
	INNER JOIN
		[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
	WHERE
		DSC.[AlbumId] = @AlbumId

END