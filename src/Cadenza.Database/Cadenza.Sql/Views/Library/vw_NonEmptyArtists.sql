CREATE VIEW [Library].[vw_NonEmptyArtists]
AS
SELECT [ArtistId]
FROM [Library].[Albums]
UNION
SELECT [ArtistId]
FROM [Library].[Tracks]