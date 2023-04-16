CREATE PROCEDURE [Queue].[GetArtistUpdates]
	@SourceId INT
AS
BEGIN

	SELECT 
		UPD.[Id],
		UPD.[SourceId],
		ART.[Id] [ArtistId],
		PRP.[Name] [PropertyName],
		UPD.[OriginalValue],
		UPD.[UpdatedValue]
	FROM
		[Queue].[ArtistUpdates] UPD
	INNER JOIN
		[Admin].[ArtistProperties] PRP ON PRP.[Id] = UPD.[PropertyId]
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = UPD.[ArtistId]
	WHERE
		UPD.[SourceId] = @SourceId
	AND
		UPD.[DateProcessed] IS NULL
	AND 
		UPD.[DateRemoved] IS NULL
	AND
		UPD.[DateErrored] IS NULL

END