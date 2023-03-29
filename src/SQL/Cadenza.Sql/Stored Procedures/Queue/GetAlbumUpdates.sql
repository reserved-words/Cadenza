CREATE PROCEDURE [Queue].[GetAlbumUpdates]
AS
BEGIN

	SELECT 
		UPD.[AlbumId],
		PRP.[Name] [PropertyName],
		UPD.[OriginalValue],
		UPD.[UpdatedValue]
	FROM
		[Queue].[AlbumUpdates] UPD
	INNER JOIN
		[Admin].[AlbumProperties] PRP ON PRP.[Id] = UPD.[PropertyId]
	WHERE
		UPD.[DateProcessed] IS NULL
	AND 
		UPD.[DateRemoved] IS NULL
	AND
		UPD.[DateErrored] IS NULL

END