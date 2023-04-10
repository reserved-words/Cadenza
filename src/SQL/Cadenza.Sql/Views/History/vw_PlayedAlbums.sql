CREATE VIEW [History].[vw_PlayedAlbums]
AS
SELECT 
	PLA.[AlbumId], 
	MAX([PlayedAt]) [PlayedAt]
FROM
	[History].[PlayedAlbums] PLA
INNER JOIN
	[History].[PlayedItems] PLI ON PLI.[Id] = PLA.[PlayedItemId]
GROUP BY
	PLA.[AlbumId]