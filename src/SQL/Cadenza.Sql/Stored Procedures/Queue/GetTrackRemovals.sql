CREATE PROCEDURE [Queue].[GetTrackRemovals]
	@SourceId INT
AS
BEGIN

	SELECT 
		[Id] [RequestId],
		[SourceId],
		[TrackIdFromSource]
	FROM
		[Queue].[TrackRemovals]
	WHERE
		[SourceId] = @SourceId
	AND
		[DateProcessed] IS NULL
	AND 
		[DateRemoved] IS NULL
	AND
		[DateErrored] IS NULL

END