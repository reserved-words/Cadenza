CREATE PROCEDURE [Queue].[ArchiveTrackRemovals]
	@TrackId INT
AS
BEGIN

	DECLARE @RequestId INT

	WHILE EXISTS (SELECT [Id] FROM [Queue].[TrackRemovals] WHERE [TrackId] = @TrackId)
	BEGIN

		SELECT 
			@RequestId = [Id]
		FROM 
			[Queue].[TrackRemovals] 
		WHERE 
			[TrackId] = @TrackId

		EXECUTE [Queue].[ArchiveTrackRemoval] @RequestId

	END

END