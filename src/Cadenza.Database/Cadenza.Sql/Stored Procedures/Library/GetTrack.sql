CREATE PROCEDURE [Library].[GetTrack]
	@Id INT
AS
BEGIN

	SELECT 
		ALB.[SourceId] [SourceId],
		TRK.[Id] [Id],
		TRK.[IdFromSource] [IdFromSource],
		TRK.[ArtistId],
		TRK.[Title] [Title],
		ART.[Name] [ArtistName],
		TRK.[DurationSeconds] [DurationSeconds],
		DSC.[AlbumId],
		TRK.[IsLoved] [IsLoved],
		TRK.[Year] [Year],
		TRK.[Lyrics] [Lyrics],
		TAG.[TagList] [TagList]
	FROM
		[Library].[Tracks] TRK
	INNER JOIN 
		[Library].[Artists] ART ON ART.[Id] = TRK.[ArtistId]
	INNER JOIN 
		[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
	INNER JOIN 
		[Library].[Albums] ALB ON ALB.[Id] = DSC.[AlbumId]
	LEFT JOIN
		[Library].[vw_TrackTags] TAG ON TAG.[TrackId] = TRK.[Id]
	WHERE
		TRK.[Id] = @Id


END