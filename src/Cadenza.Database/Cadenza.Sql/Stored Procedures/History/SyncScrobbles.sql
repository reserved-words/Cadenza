CREATE PROCEDURE [History].[SyncScrobbles]
AS
BEGIN

	-- Two inserts here, different process depending if album artist is in the scrobble record or not

	-- First process works if album artist is present - scrobble matches view will only return one match for the artist and album title
	-- (if there are multiple initially they will be ranked in order of ReleaseTypeId, AlbumId, TrackId to prioritise studio albums)

	-- Second process works if album artist is NOT present - scrobble matches view will only return one match for the album title
	-- (if there are multiple initially they will be ranked in order of ReleaseTypeId, AlbumId, TrackId to prioritise studio albums)

	INSERT INTO 
		[History].[TrackScrobbles] ([ScrobbleId], [TrackId])
	SELECT
		SRC.[Id],
		PMT.[TrackId]
	FROM
		[History].[Scrobbles] SRC
	INNER JOIN
		[History].[vw_ScrobbleMatches_AlbumArtist] PMT ON PMT.[Track] = SRC.[Track]
			AND PMT.[Artist] = SRC.[Artist]
			AND PMT.[Album] = SRC.[Album]
			AND PMT.[AlbumArtist] = SRC.[AlbumArtist]
	LEFT JOIN
		[History].[TrackScrobbles] TSC ON TSC.[ScrobbleId] = SRC.[Id]
	WHERE
		TSC.[ScrobbleId] IS NULL
	AND
		SRC.[AlbumArtist] IS NOT NULL
	AND
		PMT.[Rank] = 1

	INSERT INTO 
		[History].[TrackScrobbles] ([ScrobbleId], [TrackId])
	SELECT
		SRC.[Id],
		PMT.[TrackId]
	FROM
		[History].[Scrobbles] SRC
	INNER JOIN
		[History].[vw_ScrobbleMatches_NoAlbumArtist] PMT ON PMT.[Track] = SRC.[Track]
			AND PMT.[Artist] = SRC.[Artist]
			AND PMT.[Album] = SRC.[Album]
	LEFT JOIN
		[History].[TrackScrobbles] TSC ON TSC.[ScrobbleId] = SRC.[Id]
	WHERE
		TSC.[ScrobbleId] IS NULL
	AND
		SRC.[AlbumArtist] IS NULL
	AND
		PMT.[Rank] = 1

END