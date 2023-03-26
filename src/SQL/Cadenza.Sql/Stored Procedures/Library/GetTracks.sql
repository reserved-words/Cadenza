CREATE PROCEDURE [Library].[GetTracks]
	@SourceId INT = NULL
AS
BEGIN

	SELECT 
		T.[Id],
		T.[IdFromSource],
		T.[ArtistId],
		T.[DiscId],
		T.[TrackNo],
		T.[Title],
		T.[DurationSeconds],
		T.[Year],
		T.[Lyrics],
		A.[Id] [AlbumId],
		D.[Index] [DiscIndex],
		A.[SourceId] [SourceId],
		ART.[NameId] [ArtistNameId],
		ART.[Name] [ArtistName]
	FROM
		[Library].[Tracks] T
	INNER JOIN 
		[Library].[Discs] D ON D.[Id] = T.[DiscId]
	INNER JOIN 
		[Library].[Albums] A ON A.[Id] = D.[AlbumId]
		AND (@SourceId IS NULL OR A.[SourceId] = @SourceId)
	INNER JOIN 
		[Library].[Artists] ART ON ART.[Id] = T.[ArtistId]


END