CREATE PROCEDURE [LastFm].[QueueLovedTrack]
	@UserId INT,
	@TrackId INT
AS
BEGIN

	IF EXISTS (SELECT [TrackId] FROM [LastFm].[LovedTracks] WHERE [UserId] = @UserId AND [TrackId] = @TrackId)
	BEGIN

		UPDATE 
			[LastFm].[LovedTracks]
		SET
			[Synced] = 0,
			[FailedAttempts] = 0,
			[LastAttempt] = NULL
		WHERE
			[UserId] = @UserId 
		AND
			[TrackId] = @TrackId

	END
	ELSE
	BEGIN

		INSERT INTO [LastFm].[LovedTracks] (
			[UserId],
			[TrackId]
		)
		VALUES (
			@UserId,
			@TrackId
		)

	END

END