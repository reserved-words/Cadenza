CREATE PROCEDURE [Queue].[GetTrackRemovals]
	@SourceId INT
AS
BEGIN

	SELECT 
		[Id] [RequestId],
		[SourceId],
		[TrackIdFromSource]
	FROM
		[Queue].[TrackRemovalSync]
	WHERE
		[SourceId] = @SourceId
	AND 
		[Synced] = 0
	AND
		[FailedAttempts] < 3

END