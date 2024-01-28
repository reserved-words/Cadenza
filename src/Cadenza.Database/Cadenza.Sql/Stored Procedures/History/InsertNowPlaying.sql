CREATE PROCEDURE [History].[InsertNowPlaying]
	@TrackId INT,
	@SecondsRemaining INT
AS
BEGIN

	IF NOT EXISTS (SELECT [Id] FROM [History].[NowPlaying])
	BEGIN
		INSERT INTO [History].[NowPlaying] (
			[Id],
			[Timestamp]
		)
		VALUES (
			1,
			GETDATE()
		)
	END

	UPDATE
		[History].[NowPlaying]
	SET
		[TrackId] = @TrackId,
		[Timestamp] = GETDATE(),
		[SecondsRemaining] = @SecondsRemaining

	EXECUTE [LastFm].[QueueNowPlaying]

END