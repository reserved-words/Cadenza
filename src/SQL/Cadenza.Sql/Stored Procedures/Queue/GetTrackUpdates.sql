CREATE PROCEDURE [Queue].[GetTrackUpdates]
AS
BEGIN

	SELECT 
		UPD.[TrackId],
		PRP.[Name] [PropertyName],
		UPD.[OriginalValue],
		UPD.[UpdatedValue]
	FROM
		[Queue].[TrackUpdates] UPD
	INNER JOIN
		[Admin].[TrackProperties] PRP ON PRP.[Id] = UPD.[PropertyId]
	WHERE
		UPD.[DateProcessed] IS NULL
	AND 
		UPD.[DateRemoved] IS NULL
	AND
		UPD.[DateErrored] IS NULL

END