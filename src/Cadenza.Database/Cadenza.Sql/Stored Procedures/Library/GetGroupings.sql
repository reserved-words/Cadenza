CREATE PROCEDURE [Library].[GetGroupings]
AS
BEGIN

	SELECT DISTINCT
		[Grouping]
	FROM
		[Library].[Artists]
	ORDER BY
		[Grouping]

END