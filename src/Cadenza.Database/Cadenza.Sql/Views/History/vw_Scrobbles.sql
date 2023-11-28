CREATE VIEW [History].[vw_Scrobbles]
AS
SELECT 
	SCR.[ScrobbledAt],
	TSC.[TrackId]
FROM 
	[History].[Scrobbles] SCR
INNER JOIN
	[History].[TrackScrobbles] TSC ON TSC.[ScrobbleId] = SCR.[Id]

