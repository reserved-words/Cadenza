CREATE PROCEDURE [History].[InsertNowPlaying]
	@TrackId INT,
	@SecondsRemaining INT,
	@Username NVARCHAR(100)
AS
BEGIN

	DECLARE @UserId INT,
			@ScrobbleId INT

	SELECT 
		@UserId = [Id]
	FROM
		[Admin].[Users]
	WHERE
		[Username] = @Username

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
		[SecondsRemaining] = @SecondsRemaining,
		[Scrobbled] = 0,
		[FailedAttempts] = 0,
		[LastAttempt] = NULL
	WHERE
		[UserId] = @UserId

END