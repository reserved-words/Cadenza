CREATE PROCEDURE [LastFm].[MarkLovedTrackUpdated]
	@TrackId INT
AS
BEGIN

	UPDATE
		[LastFm].[LovedTracks]
	SET
		[Synced] = 1,
		[LastAttempt] = GETDATE()
	WHERE
		[TrackId] = @TrackId

END