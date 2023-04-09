CREATE VIEW [Library].[vw_EmptyArtists]
AS
SELECT 
	ART.[Id] 
FROM 
	[Library].[Artists] ART
LEFT JOIN 
	[Library].[vw_NonEmptyArtists] NEM ON NEM.[ArtistId] = ART.[Id]
WHERE
	NEM.[ArtistId] IS NULL