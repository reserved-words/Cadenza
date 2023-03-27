CREATE VIEW [Library].[vw_TrackTags]
AS
SELECT [TrackId], STRING_AGG([Tag], '|') [TagList]
FROM [Library].[TrackTags]
GROUP BY [TrackId]