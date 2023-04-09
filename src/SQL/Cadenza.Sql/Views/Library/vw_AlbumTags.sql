CREATE VIEW [Library].[vw_AlbumTags]
AS
SELECT [AlbumId], STRING_AGG([Tag], '|') [TagList]
FROM [Library].[AlbumTags]
GROUP BY [AlbumId]