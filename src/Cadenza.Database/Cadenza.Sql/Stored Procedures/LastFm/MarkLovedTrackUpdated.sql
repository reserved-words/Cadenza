CREATE PROCEDURE [LastFm].[MarkLovedTrackUpdated]
	@UserId INT,
	@TrackId INT
AS
BEGIN

	UPDATE
		[LastFm].[LovedTracks]
	SET
		[Synced] = 1,
		[LastAttempt] = GETDATE()
	WHERE
		[UserId] = @UserId
	AND
		[TrackId] = @TrackId

END