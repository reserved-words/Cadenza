CREATE PROCEDURE [Update].[UpdateTrackIsLoved]
	@TrackId INT,
	@IsLoved BIT
AS
BEGIN

	UPDATE
		[Library].[Tracks]
	SET
		[IsLoved] = @IsLoved
	WHERE
		[Id] = @TrackId

	EXECUTE [LastFm].[UpdateLovedTrack] @TrackId, @IsLoved

END