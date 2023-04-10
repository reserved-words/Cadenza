CREATE PROCEDURE [History].[LogTrackPlay]
	@IdFromSource NVARCHAR(500)
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
	SELECT
		@PlayedItemId,
		[Id]
	FROM
		[Library].[Tracks]
	WHERE
		[IdFromSource] = @IdFromSource

END