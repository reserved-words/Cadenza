CREATE PROCEDURE [Queue].[GetAlbumUpdates]
	@SourceId INT
AS
BEGIN

	SELECT 
		UPD.[Id],
		UPD.[SourceId],
		UPD.[AlbumId],
		PRP.[Name] [PropertyName],
		UPD.[OriginalValue],
		UPD.[UpdatedValue]
	FROM
		[Queue].[AlbumUpdates] UPD
	INNER JOIN
		[Admin].[AlbumProperties] PRP ON PRP.[Id] = UPD.[PropertyId]
	WHERE
		UPD.[SourceId] = @SourceId
	AND
		UPD.[DateProcessed] IS NULL
	AND 
		UPD.[DateRemoved] IS NULL
	AND
		UPD.[DateErrored] IS NULL

END