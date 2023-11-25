CREATE PROCEDURE [Search].[GetTracks]
AS
BEGIN

	SELECT 
		TRK.[Id],
		TAR.[Name] [ArtistName],
		TRK.[Title],
		AAR.[Name] [AlbumArtistName],
		ALB.[Title] [AlbumTitle]
	FROM
		[Library].[Tracks] TRK
	INNER JOIN
		[Library].[Artists] TAR ON TAR.[Id] = TRK.[ArtistId]
	INNER JOIN
		[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
	INNER JOIN
		[Library].[Albums] ALB ON ALB.[Id] = DSC.[AlbumId]
	INNER JOIN
		[Library].[Artists] AAR ON AAR.[Id] = ALB.[ArtistId]

END