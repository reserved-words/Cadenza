CREATE PROCEDURE [Play].[LogTagRequest]
	@Tag NVARCHAR(200)
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
		[Name] = 'Tag'

	SET @PlayedItemId = SCOPE_IDENTITY()

	INSERT INTO [History].[PlayedTags] (
		[PlayedItemId],
		[Tag])
	VALUES (
		@PlayedItemId,
		@Tag)

END