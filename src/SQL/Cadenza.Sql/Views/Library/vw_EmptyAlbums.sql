CREATE VIEW [Library].[vw_EmptyAlbums]
AS
SELECT 
	ALB.[Id] 
FROM 
	[Library].[Albums] ALB
LEFT JOIN 
	[Library].[Discs] DSC ON DSC.[AlbumId] = ALB.[Id]
WHERE
	DSC.[Id] IS NULL