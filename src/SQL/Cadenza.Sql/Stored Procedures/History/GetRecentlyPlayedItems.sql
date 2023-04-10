CREATE PROCEDURE [History].[GetRecentlyPlayedItems]
	@Max INT
AS
BEGIN

	DECLARE		
		@All INT = 0,
		@Artist INT = 1,
		@Album INT = 2,
		@Track INT = 3,
		@Genre INT = 4,
		@Grouping INT = 5,
		@Tag INT = 6

	DECLARE @Items TABLE (
		[TypeId] INT,
		[ItemIdAsInt] INT,
		[ItemIdAsString] NVARCHAR(200)
	)

	INSERT INTO @Items (
		[TypeId],
		[ItemIdAsInt],
		[ItemIdAsString]
	)
	SELECT TOP (@Max)
		[PlaylistTypeId],
		CASE 
			WHEN [PlaylistTypeId] NOT IN (@Genre, @Tag) THEN CAST([PlaylistItemId] AS INT)
			ELSE NULL
		END [ItemIdAsInt],
		CASE 
			WHEN [PlaylistTypeId] IN (@Genre, @Tag) THEN [PlaylistItemId]
			ELSE NULL
		END [ItemIdAsString]
	FROM
		[History].[vw_MostRecentPlays]
	WHERE
		[PlaylistTypeId] != @All
	ORDER BY
		[PlayedAt] DESC

	SELECT 
		PLY.[TypeId],
		CASE PLY.[TypeId]
			WHEN @Artist THEN ART.[NameId] -- artist
			WHEN @Album THEN CAST(ALB.[Id] AS NVARCHAR) -- album
			WHEN @Track THEN TRK.[IdFromSource] -- track
			WHEN @Genre THEN PLY.[ItemIdAsString] -- genre
			WHEN @Grouping THEN CAST(GRP.[Id] AS NVARCHAR) -- grouping
			WHEN @Tag THEN PLY.[ItemIdAsString] -- tag
		END [ItemId],
		CASE PLY.[TypeId]
			WHEN @Artist THEN ART.[Name] -- artist
			WHEN @Album THEN ALB.[Title] -- album
			WHEN @Track THEN TRK.[Title] -- track
			WHEN @Genre THEN PLY.[ItemIdAsString] -- genre
			WHEN @Grouping THEN GRP.[Name] -- grouping
			WHEN @Tag THEN PLY.[ItemIdAsString] -- tag
		END [PlaylistName],
		ARTIST.[Name] [ArtistName],
		ALBUM.[Title] [AlbumTitle]
	FROM
		@Items PLY
	LEFT JOIN 
		[Library].[Artists] ART ON PLY.TypeId = @Artist AND ART.[Id] = PLY.[ItemIdAsInt]
	LEFT JOIN 
		[Library].[Albums] ALB ON PLY.TypeId = @Album AND ALB.[Id] = PLY.[ItemIdAsInt]
	LEFT JOIN 
		[Library].[Tracks] TRK ON PLY.TypeId = @Track AND TRK.[Id] = PLY.[ItemIdAsInt]
	LEFT JOIN 
		[Admin].[Groupings] GRP ON PLY.TypeId = @Grouping AND GRP.[Id] = PLY.[ItemIdAsInt]
	LEFT JOIN
		[Library].[Artists] ARTIST ON 
			(PLY.[TypeId] = @Album AND ARTIST.[Id] = ALB.[ArtistId])
			OR 
			(PLY.[TypeId] = @Track AND ARTIST.[Id] = TRK.[ArtistId])
	LEFT JOIN
		[Library].[Discs] DISC ON DISC.[Id] = TRK.[DiscId]
	LEFT JOIN
		[Library].[Albums] ALBUM ON ALBUM.[Id] = DISC.[AlbumId]
END

