CREATE PROCEDURE [Library].[GetArtistImage]
	@Id INT
AS
BEGIN

	SELECT 
		IMG.[MimeType],
		IMG.[Content]
	FROM 
		[Library].[Artists] ART
	LEFT JOIN
		[Library].[ArtistImages] IMG ON IMG.[ArtistId] = ART.[Id]
	WHERE 
		ART.[Id] = @Id

END