CREATE PROCEDURE [Play].[GetArtist]
	@Id INT
AS
BEGIN

	SELECT 
		ART.[Name]
	FROM
		[Library].[Artists] ART
	WHERE
		ART.[Id] = @Id

END