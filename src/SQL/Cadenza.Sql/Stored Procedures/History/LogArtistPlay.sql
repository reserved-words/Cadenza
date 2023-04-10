CREATE PROCEDURE [History].[LogArtistPlay]
	@NameId NVARCHAR(200)
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
	SELECT
		@PlayedItemId,
		[Id]
	FROM
		[Library].[Artists]
	WHERE
		[NameId] = @NameId

END