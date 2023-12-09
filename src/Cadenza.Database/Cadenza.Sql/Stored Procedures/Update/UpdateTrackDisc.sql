CREATE PROCEDURE [Update].[UpdateTrackDisc]
	@TrackId INT,
	@DiscNo INT,
	@TrackCount INT
AS
BEGIN

	DECLARE @DiscId INT,
			@AlbumId INT

	SELECT 
		@AlbumId = [AlbumId]
	FROM
		[Library].[Tracks] T
	INNER JOIN
		[Library].[Discs] D ON D.[Id] = T.[DiscId]
	WHERE
		T.[Id] = @TrackId

	EXECUTE [Update].[AddDisc] @AlbumId, @DiscNo, @TrackCount, @DiscId OUTPUT

	UPDATE
		[Library].[Discs]
	SET
		[TrackCount] = @TrackCount
	WHERE
		[Id] = @DiscId

	UPDATE
		[Library].[Tracks]
	SET
		[DiscId] = @DiscId
	WHERE
		[Id] = @TrackId

	EXECUTE [Update].[DeleteEmptyDiscs]

END