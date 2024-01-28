CREATE PROCEDURE [LastFm].[MarkLovedTrackFailed]
	@TrackId INT
AS
BEGIN

	UPDATE
		[LastFm].[LovedTracks]
	SET
		[Synced] = 0,
		[FailedAttempts] = [FailedAttempts] + 1
	WHERE
		[TrackId] = @TrackId

END