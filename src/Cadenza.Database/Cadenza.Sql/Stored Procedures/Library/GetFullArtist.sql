CREATE PROCEDURE [Library].[GetFullArtist]
	@Id INT
AS
BEGIN

	SELECT 
		ART.[Id],
		ART.[Name],
		ART.[Grouping],
		ART.[Genre],
		ART.[City],
		ART.[State],
		ART.[Country],
		TAG.[TagList]
	FROM 
		[Library].[Artists] ART
	LEFT JOIN
		[Library].[vw_ArtistTags] TAG ON TAG.[ArtistId] = ART.[Id]
	WHERE
		ART.[Id] = @Id

END