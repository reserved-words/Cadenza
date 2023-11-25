CREATE PROCEDURE [Update].[AddDisc]
	@AlbumId INT,
	@DiscNo INT,
	@TrackCount INT,
	@Id INT OUTPUT
AS
BEGIN

	DECLARE @DiscsOnAlbum INT

	SELECT @Id = [Id] 
	FROM [Library].[Discs] 
	WHERE [AlbumId] = @AlbumId
		AND [DiscNo] = @DiscNo

	IF @Id IS NOT NULL
		RETURN

	INSERT INTO [Library].[Discs] (
		[AlbumId],
		[DiscNo],
		[TrackCount]
	)
	VALUES (
		@AlbumId,
		@DiscNo,
		@TrackCount
	)

	SET @Id = SCOPE_IDENTITY()

	EXECUTE [Update].[UpdateDiscCount] @AlbumId

END