CREATE PROCEDURE [LastFm].[UpdateLovedTrack]
	@TrackId INT,
	@IsLoved BIT
AS
BEGIN

	IF EXISTS (SELECT [TrackId] FROM [LastFm].[LovedTracks] WHERE [TrackId] = @TrackId)
	BEGIN

		UPDATE 
			[LastFm].[LovedTracks]
		SET
			[IsLoved] = @IsLoved,
			[Synced] = 0,
			[FailedAttempts] = 0,
			[LastAttempt] = NULL
		WHERE
			[TrackId] = @TrackId

	END
	ELSE
	BEGIN

		INSERT INTO [LastFm].[LovedTracks] (
			[TrackId],
			[IsLoved]
		)
		VALUES (
			@TrackId,
			@IsLoved
		)

	END

END