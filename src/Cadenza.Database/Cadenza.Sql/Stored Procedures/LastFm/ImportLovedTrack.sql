CREATE PROCEDURE [LastFm].[ImportLovedTrack]
	@Track NVARCHAR(200),
	@Artist NVARCHAR(200)
AS
BEGIN

	INSERT INTO [LastFm].[ImportedLovedTracks] (
		[Track],
		[Artist]
	)
	VALUES (
		@Track,
		@Artist
	)

END