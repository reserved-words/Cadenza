CREATE PROCEDURE [Queue].[GetArtistUpdates]
AS
BEGIN

	SELECT 
		UPD.[ArtistId],
		PRP.[Name] [PropertyName],
		UPD.[OriginalValue],
		UPD.[UpdatedValue]
	FROM
		[Queue].[ArtistUpdates] UPD
	INNER JOIN
		[Admin].[ArtistProperties] PRP ON PRP.[Id] = UPD.[PropertyId]
	WHERE
		UPD.[DateProcessed] IS NULL
	AND 
		UPD.[DateRemoved] IS NULL
	AND
		UPD.[DateErrored] IS NULL

END