CREATE PROCEDURE [Library].[GetArtist]
	@Id INT
AS
BEGIN

	SELECT 
		ART.[Id],
		ART.[Name],
		ART.[GroupingId],
		GRP.[Name] [GroupingName],
		ART.[Genre],
		ART.[City],
		ART.[State],
		ART.[Country],
		TAG.[TagList]
	FROM 
		[Library].[Artists] ART
	INNER JOIN
		[Admin].[Groupings] GRP ON GRP.[Id] = ART.[GroupingId]
	LEFT JOIN
		[Library].[vw_ArtistTags] TAG ON TAG.[ArtistId] = ART.[Id]
	WHERE
		ART.[Id] = @Id

END