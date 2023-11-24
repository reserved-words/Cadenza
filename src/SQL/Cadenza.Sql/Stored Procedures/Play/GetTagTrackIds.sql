CREATE PROCEDURE [Play].[GetTagTrackIds]
	@Tag NVARCHAR(200),
	@LogRequest BIT
AS
BEGIN

	IF @LogRequest = 1
	BEGIN
		EXECUTE [Play].[LogTagRequest] @Tag
	END

	SELECT 
		[TrackId]
	FROM
		[Library].[TrackTags]
	WHERE
		[Tag] = @Tag

	UNION

	SELECT
		TRK.[Id]
	FROM
		[Library].[Tracks] TRK
	INNER JOIN
		[Library].[ArtistTags] TAG ON TAG.[ArtistId] = TRK.[ArtistId]
	WHERE
		TAG.[Tag] = @Tag

	UNION

	SELECT
		TRK.[Id]
	FROM
		[Library].[Tracks] TRK
	INNER JOIN 
		[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
	INNER JOIN
		[Library].[AlbumTags] TAG ON TAG.[AlbumId] = DSC.[AlbumId]
	WHERE
		TAG.[Tag] = @Tag

END