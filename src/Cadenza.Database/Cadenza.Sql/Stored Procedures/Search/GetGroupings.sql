CREATE PROCEDURE [Search].[GetGroupings]
AS
BEGIN

	SELECT
		[Id],
		[Name]
	FROM
		[Admin].[Groupings]

END