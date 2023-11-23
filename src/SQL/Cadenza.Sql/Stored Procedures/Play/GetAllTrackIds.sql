CREATE PROCEDURE [Play].[GetAllTrackIds]
AS
BEGIN

	SELECT 
		[Id]
	FROM
		[Library].[Tracks]

END