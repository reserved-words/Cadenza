CREATE PROCEDURE [Admin].[SaveLastFmSessionKey]
	@Username NVARCHAR(100),
	@LastFmUsername NVARCHAR(100),
	@LastFmSessionKey NVARCHAR(100)
AS
BEGIN

	UPDATE
		[Admin].[Users]
	SET
		[LastFmUsername] = @LastFmUsername,
		[LastFmSessionKey] = @LastFmSessionKey
	WHERE
		[Username] = @Username

END