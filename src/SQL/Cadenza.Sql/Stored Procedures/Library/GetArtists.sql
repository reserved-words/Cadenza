CREATE PROCEDURE [Library].[GetArtists]
AS
BEGIN

	SELECT 
		ART.[Id],
		ART.[NameId],
		ART.[Name],
		ART.[GroupingId],
		ART.[Genre],
		ART.[City],
		ART.[State],
		ART.[Country],
		TAG.[TagList]
	FROM 
		[Library].[Artists] ART
	LEFT JOIN
		[Library].[vw_ArtistTags] TAG ON TAG.[ArtistId] = ART.[Id]

END