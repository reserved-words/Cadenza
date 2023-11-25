CREATE PROCEDURE [History].[GetNewScrobbles]
AS
BEGIN

	SELECT
		[ScrobbledAt],
		[Track],
		[Artist],
		[Album],
		[AlbumArtist]
	FROM
		[History].[Scrobbles]
	WHERE
		[Scrobbled] = 0

END