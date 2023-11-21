CREATE PROCEDURE [Library].[UpdateTrackDisc]
	@TrackId INT,
	@DiscIndex INT
AS
BEGIN

	DECLARE @DiscId INT,
			@AlbumId INT,
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

	EXECUTE [Library].[UpdateTrackCount] @DiscId

	EXECUTE [Library].[DeleteEmptyDiscs]

END