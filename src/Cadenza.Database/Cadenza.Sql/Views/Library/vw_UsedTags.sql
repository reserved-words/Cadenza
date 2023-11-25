CREATE VIEW [Library].[vw_UsedTags]
AS
SELECT [Tag] FROM [Library].[ArtistTags]
UNION
SELECT [Tag] FROM [Library].[AlbumTags]
UNION
SELECT [Tag] FROM [Library].[TrackTags]