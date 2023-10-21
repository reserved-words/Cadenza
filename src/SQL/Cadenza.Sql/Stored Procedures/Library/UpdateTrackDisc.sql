CREATE PROCEDURE [Library].[UpdateTrackDisc]
	@TrackId INT,
	@DiscIndex INT
AS
BEGIN

	DECLARE @DiscId INT,
			@AlbumId INT,
			@TrackCount INT,
			@TracksOnDisc INT

	SELECT 
		@AlbumId = [AlbumId]
	FROM
		[Library].[Tracks] T
	INNER JOIN
		[Library].[Discs] D ON D.[Id] = T.[DiscId]
	WHERE
		T.[Id] = @TrackId

	EXECUTE [Library].[AddDisc] @AlbumId, @DiscIndex, 1, @DiscId OUTPUT

	UPDATE
		[Library].[Tracks]
	SET
		[DiscId] = @DiscId
	WHERE
		[Id] = @TrackId

	SELECT
		@TrackCount = [TrackCount]
	FROM
		[Library].[Discs]
	WHERE
		[Id] = @DiscId

	SELECT 
		@TracksOnDisc = COUNT([Id])
	FROM
		[Library].[Tracks]
	WHERE
		[DiscId] = @DiscId

	IF @TracksOnDisc > @TrackCount
	BEGIN
		UPDATE
			[Library].[Discs]
		SET
			@TrackCount = @TracksOnDisc
		WHERE
			[Id] = @DiscId
	END

	EXECUTE [Library].[DeleteEmptyDiscs]

END