CREATE PROCEDURE [LastFm].[ImportScrobbles]
AS
BEGIN

	INSERT INTO [History].[Scrobbles] (
		[ScrobbledAt], 
		[Track], 
		[Artist], 
		[Album])
	SELECT 
		[ScrobbledAt], 
		[Track], 
		[Artist], 
		[Album] 
	FROM 
		[LastFm].[ImportedScrobbles]

END