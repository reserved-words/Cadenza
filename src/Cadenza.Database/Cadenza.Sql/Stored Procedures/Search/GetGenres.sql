CREATE PROCEDURE [Search].[GetGenres]
AS
BEGIN

	SELECT DISTINCT
		[Grouping], 
		[Genre]
	FROM
		[Library].[Artists]

END