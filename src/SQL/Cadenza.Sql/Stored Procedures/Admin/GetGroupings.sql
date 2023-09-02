CREATE PROCEDURE [Admin].[GetGroupings]
AS
BEGIN

	SELECT [Id], [Name]
	FROM [Admin].[Groupings]
	ORDER BY [Name]

END

