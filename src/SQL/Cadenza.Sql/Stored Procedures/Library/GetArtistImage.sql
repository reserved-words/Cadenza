CREATE PROCEDURE [Library].[GetArtistImage]
	@NameId NVARCHAR(200)
AS
BEGIN

	SELECT 
		IMG.[Image]
	FROM 
		[Library].[Artists] ART
	LEFT JOIN
		[Library].[ArtistImages] IMG ON IMG.[ArtistId] = ART.[Id]
	WHERE 
		ART.[NameId] = @NameId

END