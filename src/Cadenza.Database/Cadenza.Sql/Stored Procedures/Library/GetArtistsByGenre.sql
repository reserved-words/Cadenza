CREATE PROCEDURE [Library].[GetArtistsByGenre]
	@Genre NVARCHAR(100),
	@Grouping NVARCHAR(50)
AS
BEGIN

	SELECT 
		ART.[Id],
		ART.[Name],
		ART.[Grouping],
		ART.[Genre]
	FROM 
		[Library].[Artists] ART
	WHERE
		ART.[Genre] = @Genre
	AND
		ART.[Grouping] = @Grouping

END