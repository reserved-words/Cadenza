CREATE PROCEDURE [Search].[GetArtists]
AS
BEGIN

	SELECT 
		[Id],
		[Name]
	FROM
		[Library].[Artists]

END