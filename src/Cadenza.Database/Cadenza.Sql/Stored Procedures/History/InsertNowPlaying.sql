CREATE PROCEDURE [History].[InsertNowPlaying]
	@TrackId INT,
	@SecondsRemaining INT,
	@Username NVARCHAR(100)
AS
BEGIN

	DECLARE @UserId INT = [Admin].[GetUserId](@Username)

	IF NOT EXISTS (SELECT [UserId] FROM [History].[NowPlaying] WHERE [UserId] = @UserId)
	BEGIN
		INSERT INTO [History].[NowPlaying] (
			[UserId],
			[Timestamp]
		)
		VALUES (
			@UserId,
			GETDATE()
		)
	END

	UPDATE
		[History].[NowPlaying]
	SET
		[TrackId] = @TrackId,
		[Timestamp] = GETDATE(),
		[SecondsRemaining] = @SecondsRemaining
	WHERE
		[UserId] = @UserId

	EXECUTE [LastFm].[QueueNowPlaying] @UserId

END