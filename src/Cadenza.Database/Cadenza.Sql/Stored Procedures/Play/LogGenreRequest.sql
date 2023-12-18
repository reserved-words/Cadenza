CREATE PROCEDURE [Play].[LogGenreRequest]
	@Genre NVARCHAR(100),
	@GroupingId INT
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
		[Name] = 'Genre'

	SET @PlayedItemId = SCOPE_IDENTITY()

	INSERT INTO [History].[PlayedGenres] (
		[PlayedItemId],
		[GenreId],
		[GroupingId])
	VALUES (
		@PlayedItemId,
		@Genre,
		@GroupingId)

END