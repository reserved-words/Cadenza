CREATE PROCEDURE [LastFm].[ImportScrobbles]
	@UserId INT
AS
BEGIN

	INSERT INTO [History].[Scrobbles] (
		[UserId], 
		[ScrobbledAt], 
		[Track], 
		[Artist], 
		[Album])
	SELECT 
		@UserId, 
		[ScrobbledAt], 
		[Track], 
		[Artist], 
		[Album] 
	FROM 
		[LastFm].[ImportedScrobbles]

END