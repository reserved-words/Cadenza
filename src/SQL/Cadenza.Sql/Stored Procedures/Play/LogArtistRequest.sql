CREATE PROCEDURE [Play].[LogArtistRequest]
	@ArtistId INT
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
		[Name] = 'Artist'

	SET @PlayedItemId = SCOPE_IDENTITY()

	INSERT INTO [History].[PlayedArtists] (
		[PlayedItemId],
		[ArtistId])
	VALUES (
		@PlayedItemId,
		@ArtistId)

END