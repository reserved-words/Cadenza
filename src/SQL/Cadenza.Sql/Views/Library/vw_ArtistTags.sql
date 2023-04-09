CREATE VIEW [Library].[vw_ArtistTags]
AS
SELECT [ArtistId], STRING_AGG([Tag], '|') [TagList]
FROM [Library].[ArtistTags]
GROUP BY [ArtistId]