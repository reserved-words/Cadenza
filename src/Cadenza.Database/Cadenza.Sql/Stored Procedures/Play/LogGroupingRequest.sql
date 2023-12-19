CREATE PROCEDURE [Play].[LogGroupingRequest]
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
		[Name] = 'Grouping'

	SET @PlayedItemId = SCOPE_IDENTITY()

	INSERT INTO [History].[PlayedGroupings] (
		[PlayedItemId],
		[Grouping])
	VALUES (
		@PlayedItemId,
		@Grouping)

END