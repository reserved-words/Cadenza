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
		[FailedAttempts] < 3

END