CREATE PROCEDURE [History].[SyncScrobbles]
AS
BEGIN

	INSERT INTO 
		[History].[TrackScrobbles] ([ScrobbleId], [TrackId])
	SELECT
		SRC.[Id],
		PMT.[TrackId]
	FROM
		[History].[Scrobbles] SRC
	INNER JOIN
		[History].[vw_PotentialScrobbleMatches] PMT ON PMT.[Track] = SRC.[Track]
			AND PMT.[Artist] = SRC.[Artist]
			AND PMT.[Album] = SRC.[Album]
			AND PMT.[AlbumArtist] = SRC.[AlbumArtist]
	LEFT JOIN
		[History].[TrackScrobbles] TSC ON TSC.[ScrobbleId] = SRC.[Id]
	WHERE
		TSC.[ScrobbleId] IS NULL
	AND
		PMT.[Rank] = 1

END