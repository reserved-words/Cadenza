CREATE PROCEDURE [Admin].[SaveLastFmSessionKey]
	@LastFmUsername NVARCHAR(100),
	@LastFmSessionKey NVARCHAR(100)
AS
BEGIN

	UPDATE
		[Admin].[User]
	SET
		[LastFmUsername] = @LastFmUsername,
		[LastFmSessionKey] = @LastFmSessionKey

END