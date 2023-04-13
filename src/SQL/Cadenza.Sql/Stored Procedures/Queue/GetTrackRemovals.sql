CREATE PROCEDURE [Queue].[GetTrackRemovals]
	@SourceId INT
AS
BEGIN

	SELECT 
		REM.[Id],
		REM.[Name],
		REM.[SourceId],
		TRK.[IdFromSource] [TrackIdFromSource]
	FROM
		[Queue].[TrackRemovals] REM
	INNER JOIN
		[Library].[Tracks] TRK ON TRK.[Id] = REM.[TrackId]
	WHERE
		REM.[SourceId] = @SourceId
	AND
		REM.[DateProcessed] IS NULL
	AND 
		REM.[DateRemoved] IS NULL
	AND
		REM.[DateErrored] IS NULL

END