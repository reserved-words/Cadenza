CREATE VIEW [History].[vw_MostRecentPlays]
AS
SELECT 
	[PlaylistTypeId], 
	[PlaylistItemId], 
	MAX([PlayedAt]) [PlayedAt]
FROM
	[History].[vw_PlayedItems]
GROUP BY
	[PlaylistTypeId], 
	[PlaylistItemId]