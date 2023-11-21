CREATE PROCEDURE [Library].[UpdateDiscCount]
	@AlbumId INT
AS
BEGIN

	DECLARE @DiscsOnAlbum INT

	SELECT 
		@DiscsOnAlbum = COUNT([Id])
	FROM
		[Library].[Discs]
	WHERE
		[AlbumId] = @AlbumId

	UPDATE
		[Library].[Albums]
	SET
		[DiscCount] = GREATEST([DiscCount], @DiscsOnAlbum)
	WHERE
		[Id] = @AlbumId

END