CREATE PROCEDURE [LastFm].[QueueScrobble]
	@ScrobbleId INT
AS
BEGIN

	INSERT INTO [LastFm].[Scrobbles] (
		[ScrobbleId]
	)
	VALUES (
		@ScrobbleId
	)

END