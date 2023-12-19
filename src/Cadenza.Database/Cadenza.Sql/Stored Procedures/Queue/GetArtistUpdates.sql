CREATE PROCEDURE [Queue].[GetArtistUpdates]
	@SourceId INT
AS
BEGIN

	SELECT 
		SNC.[ArtistId],
		ART.[Name],
		ART.[Grouping],
		ART.[Genre],
		ART.[City],
		ART.[State],
		ART.[Country],
		IMG.[MimeType] [ImageMimeType],
		IMG.[Content] [ImageContent],
		TAG.[TagList]
	FROM
		[Queue].[ArtistSync] SNC
	INNER JOIN
		[Library].[Artists] ART ON SNC.[ArtistId] = ART.[Id]
	LEFT JOIN
		[Library].[vw_ArtistTags] TAG ON TAG.[ArtistId] = ART.[Id]
	LEFT JOIN
		[Library].[ArtistImages] IMG ON IMG.[ArtistId] = ART.[Id]
	WHERE
		(SNC.[LastSynced] IS NULL OR SNC.[LastSynced] < SNC.[LastUpdated])
	AND
		SNC.[FailedAttempts] < 3

END