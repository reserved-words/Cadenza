CREATE VIEW [Library].[vw_EmptyDiscs]
AS
SELECT 
	DSC.[Id] 
FROM 
	[Library].[Discs] DSC
LEFT JOIN 
	[Library].[Tracks] TRK ON TRK.[DiscId] = DSC.[Id]
WHERE
	TRK.[Id] IS NULL