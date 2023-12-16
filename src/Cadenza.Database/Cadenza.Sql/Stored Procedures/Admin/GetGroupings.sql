CREATE PROCEDURE [Admin].[GetGroupings]
AS
BEGIN

	WITH [UsedGroupings] AS (
		SELECT DISTINCT 
			[GroupingId]
		FROM 
			[Library].[Artists]
	)
	SELECT 
		GRP.[Id], 
		GRP.[Name], 
		CASE WHEN USD.[GroupingId] IS NULL THEN 0 ELSE 1 END [IsUsed]
	FROM 
		[Admin].[Groupings] GRP
	LEFT JOIN
		[UsedGroupings] USD ON USD.[GroupingId] = GRP.[Id]
	ORDER BY 
		GRP.[Name]

END

