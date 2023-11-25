CREATE PROCEDURE [Play].[LogGroupingRequest]
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
		[Name] = 'Grouping'

	SET @PlayedItemId = SCOPE_IDENTITY()

	INSERT INTO [History].[PlayedGroupings] (
		[PlayedItemId],
		[GroupingId])
	VALUES (
		@PlayedItemId,
		@GroupingId)

END