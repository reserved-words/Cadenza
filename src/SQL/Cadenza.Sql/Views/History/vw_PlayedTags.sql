CREATE VIEW [History].[vw_PlayedTags]
AS
SELECT 
	PLA.[Tag], 
	MAX([PlayedAt]) [PlayedAt]
FROM
	[History].[PlayedTags] PLA
INNER JOIN
	[History].[PlayedItems] PLI ON PLI.[Id] = PLA.[PlayedItemId]
GROUP BY
	PLA.[Tag]