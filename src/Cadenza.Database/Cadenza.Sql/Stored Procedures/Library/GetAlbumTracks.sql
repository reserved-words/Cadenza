CREATE PROCEDURE [Library].[GetAlbumTracks]
	@Id INT
AS
BEGIN

	SELECT 
		TRK.[Id] [TrackId],
		TRK.[IdFromSource],
		TRK.[Title],
		TRK.[ArtistId],
		ART.[Name] [ArtistName],
		TRK.[DurationSeconds],
		DSC.[Index] [DiscNo],
		TRK.[TrackNo]
	FROM
		[Library].[Tracks] TRK
	INNER JOIN 
		[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
	INNER JOIN 
		[Library].[Artists] ART ON ART.[Id] = TRK.[ArtistId]
	WHERE
		DSC.[AlbumId] = @Id

END