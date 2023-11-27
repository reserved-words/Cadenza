CREATE PROCEDURE [Update].[UpdateTrackIsLoved]
	@Username NVARCHAR(100),
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

	DECLARE @UserId INT = [Admin].[GetUserId](@Username)
	EXECUTE [LastFm].[QueueLovedTrack] @UserId, @TrackId

END