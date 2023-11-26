CREATE VIEW [History].[vw_ScrobbleMatches_AlbumArtist]
AS
SELECT 
	TRK.[Id] [TrackId], 
	TRK.[Title] [Track],
	ART.[Name] [Artist],
	ALB.[Title] [Album],
	ALA.[Name] [AlbumArtist],
	DENSE_RANK() OVER (PARTITION BY TRK.[Title], ART.[Name], ALB.[Title], ALA.[Name] ORDER BY ALB.[ReleaseTypeId], ALB.[Id], TRK.[Id]) [Rank]
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
