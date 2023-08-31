CREATE PROCEDURE [History].[LogTrackPlay]
	@TrackId INT
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
		[Name] = 'Track'

	SET @PlayedItemId = SCOPE_IDENTITY()

	INSERT INTO [History].[PlayedTracks] (
		[PlayedItemId],
		[TrackId])
	VALUES (
		@PlayedItemId,
		@TrackId)

END