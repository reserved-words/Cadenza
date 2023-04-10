CREATE PROCEDURE [History].[AddPlayedAlbum]
	@AlbumId INT
AS
BEGIN
	
	DECLARE @PlayedItemId INT
	
	INSERT INTO [History].[PlayedItems] (
		[PlaylistTypeId])
	SELECT 
		[Id]
	FROM
		[Admin].[PlaylistTypes]
	WHERE
		[Name] = 'Album'

	SET @PlayedItemId = SCOPE_IDENTITY()

	INSERT INTO [History].[PlayedAlbums] (
		[PlayedItemId],
		[AlbumId])
	VALUES (
		@PlayedItemId,
		@AlbumId)

END