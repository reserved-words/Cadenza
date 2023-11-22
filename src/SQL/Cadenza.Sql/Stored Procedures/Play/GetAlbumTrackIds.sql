CREATE PROCEDURE [Play].[GetAlbumTrackIds]
	@Id INT
AS
BEGIN

	SELECT 
		TRK.[Id]
	FROM
		[Library].[Tracks] TRK
	INNER JOIN 
		[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
	WHERE
		DSC.[AlbumId] = @Id
	ORDER BY
		DSC.[Index],
		TRK.[TrackNo]

END