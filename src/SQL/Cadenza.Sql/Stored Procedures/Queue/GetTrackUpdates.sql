CREATE PROCEDURE [Queue].[GetTrackUpdates]
	@SourceId INT
AS
BEGIN

	SELECT 
		UPD.[Id],
		UPD.[SourceId],
		UPD.[TrackId],
		PRP.[Name] [PropertyName],
		UPD.[OriginalValue],
		UPD.[UpdatedValue]
	FROM
		[Queue].[TrackUpdates] UPD
	INNER JOIN
		[Admin].[TrackProperties] PRP ON PRP.[Id] = UPD.[PropertyId]
	WHERE
		UPD.[SourceId] = @SourceId
	AND
		UPD.[DateProcessed] IS NULL
	AND 
		UPD.[DateRemoved] IS NULL
	AND
		UPD.[DateErrored] IS NULL

END