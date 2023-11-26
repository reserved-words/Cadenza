CREATE PROCEDURE [Admin].[ImportScrobbles]
	@UserId INT
AS
BEGIN

	INSERT INTO [History].[Scrobbles] (
		[UserId], 
		[ScrobbledAt], 
		[Track], 
		[Artist], 
		[Album], 
		[Scrobbled])
	SELECT 
		@UserId, 
		[ScrobbledAt], 
		[Track], 
		[Artist], 
		[Album], 
		1 
	FROM 
		[Admin].[ImportedScrobbles]

END