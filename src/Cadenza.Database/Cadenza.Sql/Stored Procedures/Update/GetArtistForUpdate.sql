CREATE PROCEDURE [Update].[GetArtistForUpdate]
	@Id INT
AS
BEGIN

	SELECT 
		ART.[GroupingId],
		ART.[Genre],
		ART.[City],
		ART.[State],
		ART.[Country],
		IMG.[MimeType] [ImageMimeType],
		IMG.[Content] [ImageContent],
		TAG.[TagList]
	FROM 
		[Library].[Artists] ART
	INNER JOIN
		[Admin].[Groupings] GRP ON GRP.[Id] = ART.[GroupingId]
	LEFT JOIN
		[Library].[vw_ArtistTags] TAG ON TAG.[ArtistId] = ART.[Id]
	LEFT JOIN
		[Library].[ArtistImages] IMG ON IMG.[ArtistId] = ART.[Id]
	WHERE 
		ART.[Id] = @Id

END