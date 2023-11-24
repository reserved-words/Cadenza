CREATE PROCEDURE [Search].[GetGenres]
AS
BEGIN

	SELECT DISTINCT
		[Genre]
	FROM
		[Library].[Artists]

END