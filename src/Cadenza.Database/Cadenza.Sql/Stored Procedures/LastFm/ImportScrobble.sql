﻿CREATE PROCEDURE [LastFm].[ImportScrobble]
	@ScrobbledAt DATETIME,
	@Track NVARCHAR(200),
	@Artist NVARCHAR(200),
	@Album NVARCHAR(200),
	@AlbumArtist NVARCHAR(200)
AS
BEGIN

	INSERT INTO [LastFm].[ImportedScrobbles] (
		[ScrobbledAt],
		[Track],
		[Artist],
		[Album],
		[AlbumArtist]
	)
	VALUES (
		@ScrobbledAt,
		@Track,
		@Artist,
		@Album,
		@AlbumArtist
	)

END