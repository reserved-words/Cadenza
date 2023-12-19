CREATE PROCEDURE [Play].[LogGenreRequest]
	@Genre NVARCHAR(100),
	@Grouping NVARCHAR(50)
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
		[Genre],
		[Grouping])
	VALUES (
		@PlayedItemId,
		@Genre,
		@Grouping)

END