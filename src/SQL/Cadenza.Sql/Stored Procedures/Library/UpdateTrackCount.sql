CREATE PROCEDURE [Library].[UpdateTrackCount]
	@DiscId INT
AS
BEGIN

	DECLARE @TracksOnDisc INT

	SELECT 
		@TracksOnDisc = COUNT([Id])
	FROM
		[Library].[Tracks]
	WHERE
		[DiscId] = @DiscId

	UPDATE
		[Library].[Discs]
	SET
		[TrackCount] = GREATEST([TrackCount], @TracksOnDisc)
	WHERE
		[Id] = @DiscId

	EXECUTE [Library].[DeleteEmptyDiscs]

END