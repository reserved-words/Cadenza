CREATE PROCEDURE [LastFm].[QueueLovedTrack]
	@TrackId INT
AS
BEGIN

	IF EXISTS (SELECT [TrackId] FROM [LastFm].[LovedTracks] WHERE [TrackId] = @TrackId)
	BEGIN

		UPDATE 
			[LastFm].[LovedTracks]
		SET
			[Synced] = 0,
			[FailedAttempts] = 0,
			[LastAttempt] = NULL
		WHERE
			[TrackId] = @TrackId

	END
	ELSE
	BEGIN

		INSERT INTO [LastFm].[LovedTracks] (
			[TrackId]
		)
		VALUES (
			@TrackId
		)

	END

END