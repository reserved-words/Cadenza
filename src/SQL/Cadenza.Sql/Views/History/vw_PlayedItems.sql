CREATE VIEW [History].[vw_PlayedItems]
AS
SELECT 
	PLI.[PlayedAt],
	PLI.[PlaylistTypeId],
	CASE PLI.[PlaylistTypeId]
		WHEN 0 THEN NULL
		WHEN 1 THEN CAST(ART.[ArtistId] AS NVARCHAR)
		WHEN 2 THEN CAST(ALB.[AlbumId] AS NVARCHAR)
		WHEN 3 THEN CAST(TRK.[TrackId] AS NVARCHAR)
		WHEN 4 THEN GEN.[GenreId]
		WHEN 5 THEN CAST(GRP.[GroupingId] AS NVARCHAR)
		WHEN 6 THEN TAG.[Tag]
	END [PlaylistItemId]
FROM [History].[PlayedItems] PLI
	LEFT JOIN [History].[PlayedArtists] ART ON ART.[PlayedItemId] = PLI.[Id]
	LEFT JOIN [History].[PlayedAlbums] ALB ON ALB.[PlayedItemId] = PLI.[Id]
	LEFT JOIN [History].[PlayedTracks] TRK ON TRK.[PlayedItemId] = PLI.[Id]
	LEFT JOIN [History].[PlayedGroupings] GRP ON GRP.[PlayedItemId] = PLI.[Id]
	LEFT JOIN [History].[PlayedGenres] GEN ON GEN.[PlayedItemId] = PLI.[Id]
	LEFT JOIN [History].[PlayedTags] TAG ON TAG.[PlayedItemId] = PLI.[Id]