CREATE PROCEDURE [Library].[GetTaggedItems]
	@Tag NVARCHAR(200)
AS
BEGIN

	SELECT 
		'Track' [Type],
		TRK.[Id] [Id],
		TRK.[Title] [Name],
		ART.[Name] [Artist],
		ALB.[Title] [Album],
		ALA.[Name] [AlbumArtist]
	FROM
		[Library].[TrackTags] TAG
	INNER JOIN
		[Library].[Tracks] TRK ON TRK.[Id] = TAG.[TrackId]
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = TRK.[ArtistId]
	INNER JOIN 
		[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
	INNER JOIN
		[Library].[Albums] ALB ON ALB.[Id] = DSC.[AlbumId]
	INNER JOIN
		[Library].[Artists] ALA ON ALA.[Id] = ALB.[ArtistId]
	WHERE 
		TAG.[Tag] = @Tag

	UNION

	SELECT 
		'Album' [Type],
		ALB.[Id] [Id],
		ALB.[Title] [Name],
		ART.[Name] [Artist],
		NULL [Album],
		NULL [AlbumArtist]
	FROM
		[Library].[AlbumTags] TAG
	INNER JOIN
		[Library].[Albums] ALB ON ALB.[Id] = TAG.[AlbumId]
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = ALB.[ArtistId]
	WHERE 
		TAG.[Tag] = @Tag

	UNION

	SELECT 
		'Artist' [Type],
		ART.[Id] [Id],
		ART.[Name] [Name],
		NULL [Artist],
		NULL [Album],
		NULL [AlbumArtist]
	FROM
		[Library].[ArtistTags] TAG
	INNER JOIN
		[Library].[Artists] ART ON ART.[Id] = TAG.[ArtistId]
	WHERE 
		TAG.[Tag] = @Tag

END