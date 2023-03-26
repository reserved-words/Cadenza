CREATE PROCEDURE [Library].[GetArtists]
AS
BEGIN

	SELECT 
		[Id],
		[NameId],
		[Name],
		[GroupingId],
		[Genre],
		[City],
		[State],
		[Country],
		[ImageUrl]
	FROM 
		[Library].[Artists]

END