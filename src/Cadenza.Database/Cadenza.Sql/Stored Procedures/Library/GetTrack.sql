CREATE PROCEDURE [Library].[GetTrack]
	@Id INT
AS
BEGIN

	SELECT 
		ALB.[SourceId] [SourceId],
		TRK.[Id] [Id],
		TRK.[IdFromSource] [IdFromSource],
		TRK.[Title] [TrackTitle],
		TRK.[IsLoved] [IsLoved],
		TRK.[DurationSeconds] [DurationSeconds],
		TRK.[Year] [TrackYear],
		TRK.[Lyrics] [Lyrics],
		TAG.[TagList] [TrackTagList],
		DSC.[DiscNo] [DiscNo],
		TRK.[TrackNo] [TrackNo],
		ALB.[DiscCount] [DiscCount],
		DSC.[TrackCount] [TrackCount],
		TRK.[ArtistId] [ArtistId],
		ART.[Name] [ArtistName],
		ART.[GroupingId] [ArtistGroupingId],
		ARG.[Name] [ArtistGroupingName],
		ART.[Genre] [ArtistGenre],
		ART.[City] [ArtistCity],
		ART.[State] [ArtistState],
		ART.[Country] [ArtistCountry],
		TAT.[TagList] [ArtistTagList],
		DSC.[AlbumId] [AlbumId],
		ALB.[Title] [AlbumTitle],
		ALB.[ReleaseTypeId],
		ALB.[Year] [AlbumYear],
		ALT.[TagList] [AlbumTagList],
		ALB.[ArtistId] [AlbumArtistId],
		ALA.[Name] [AlbumArtistName],
		ALA.[GroupingId] [AlbumArtistGroupingId],
		ALG.[Name] [AlbumArtistGroupingName],
		ALA.[Genre] [AlbumArtistGenre],
		ALA.[City] [AlbumArtistCity],
		ALA.[State] [AlbumArtistState],
		ALA.[Country] [AlbumArtistCountry],
		AAT.[TagList] [AlbumArtistTagList]
	FROM
		[Library].[Tracks] TRK
	INNER JOIN 
		[Library].[Artists] ART ON ART.[Id] = TRK.[ArtistId]
	INNER JOIN 
		[Admin].[Groupings] ARG ON ARG.[Id] = ART.[GroupingId]
	INNER JOIN 
		[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
	INNER JOIN 
		[Library].[Albums] ALB ON ALB.[Id] = DSC.[AlbumId]
	INNER JOIN 
		[Library].[Artists] ALA ON ALA.[Id] = ALB.[ArtistId]
	INNER JOIN 
		[Admin].[Groupings] ALG ON ALG.[Id] = ALA.[GroupingId]
	LEFT JOIN
		[Library].[vw_TrackTags] TAG ON TAG.[TrackId] = TRK.[Id]
	LEFT JOIN
		[Library].[vw_ArtistTags] TAT ON TAT.[ArtistId] = TRK.[ArtistId]
	LEFT JOIN
		[Library].[vw_AlbumTags] ALT ON ALT.[AlbumId] = DSC.[AlbumId]
	LEFT JOIN
		[Library].[vw_ArtistTags] AAT ON AAT.[ArtistId] = ALB.[ArtistId]
	WHERE
		TRK.[Id] = @Id


END