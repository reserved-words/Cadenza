CREATE PROCEDURE [Play].[GetAllTrackIds]
	@LogRequest BIT
AS
BEGIN
	
	IF @LogRequest = 1
	BEGIN
		EXECUTE [Play].[LogLibraryRequest]
	END

	SELECT 
		[Id]
	FROM
		[Library].[Tracks]

END