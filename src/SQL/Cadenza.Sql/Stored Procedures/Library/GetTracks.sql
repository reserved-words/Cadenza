CREATE PROCEDURE [Library].[GetTracks]
	@SourceId INT = NULL
AS
BEGIN

	SELECT 
		TRK.[Id],
		TRK.[IdFromSource],
		TRK.[ArtistId],
		TRK.[DiscId],
		TRK.[TrackNo],
		TRK.[Title],
		TRK.[DurationSeconds],
		TRK.[Year],
		TRK.[Lyrics],
		ALB.[Id] [AlbumId],
		DSC.[Index] [DiscIndex],
		ALB.[SourceId] [SourceId],
		ART.[NameId] [ArtistNameId],
		ART.[Name] [ArtistName],
		TAG.[TagList]
	FROM
		[Library].[Tracks] TRK
	INNER JOIN 
		[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
	INNER JOIN 
		[Library].[Albums] ALB ON ALB.[Id] = DSC.[AlbumId]
		AND (@SourceId IS NULL OR ALB.[SourceId] = @SourceId)
	INNER JOIN 
		[Library].[Artists] ART ON ART.[Id] = TRK.[ArtistId]
	LEFT JOIN
		[Library].[vw_TrackTags] TAG ON TAG.[TrackId] = TRK.[Id]


END