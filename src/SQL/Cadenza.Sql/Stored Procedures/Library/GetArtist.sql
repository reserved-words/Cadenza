CREATE PROCEDURE [Library].[GetArtist]
	@Id INT
AS
BEGIN

	SELECT 
		ART.[Id],
		ART.[Name],
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
	LEFT JOIN
		[Library].[vw_ArtistTags] TAG ON TAG.[ArtistId] = ART.[Id]
	LEFT JOIN
		[Library].[ArtistImages] IMG ON IMG.[ArtistId] = ART.[Id]
	WHERE 
		ART.[Id] = @Id

END