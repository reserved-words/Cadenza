CREATE PROCEDURE [Library].[GetArtist]
	@Id INT
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
		ART.[Id] = @Id

END