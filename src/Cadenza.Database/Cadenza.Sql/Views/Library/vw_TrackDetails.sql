CREATE VIEW [Library].[vw_TrackDetails]
AS
SELECT 
	TRK.[Id] [TrackId],
	TRK.[Title] [TrackTitle],
	ART.[Id] [ArtistId],
	ART.[Name] [ArtistName],
	ALB.[Id] [AlbumId],
	ALB.[Title] [AlbumTitle],
	ALA.[Id] [AlbumArtistId],
	ALA.[Name] [AlbumArtistName]
FROM
	[Library].[Tracks] TRK
INNER JOIN
	[Library].[Artists] ART ON ART.[Id] = TRK.[ArtistId]
INNER JOIN
	[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
INNER JOIN
	[Library].[Albums] ALB ON ALB.[Id] = DSC.[AlbumId]
INNER JOIN
	[Library].[Artists] ALA ON ALA.[Id] = ALB.[ArtistId]