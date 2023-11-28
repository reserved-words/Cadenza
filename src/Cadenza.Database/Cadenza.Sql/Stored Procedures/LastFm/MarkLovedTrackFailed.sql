CREATE PROCEDURE [LastFm].[MarkLovedTrackFailed]
	@UserId INT,
	@TrackId INT
AS
BEGIN

	UPDATE
		[LastFm].[LovedTracks]
	SET
		[Synced] = 0,
		[FailedAttempts] = [FailedAttempts] + 1
	WHERE
		[UserId] = @UserId
	AND
		[TrackId] = @TrackId

END