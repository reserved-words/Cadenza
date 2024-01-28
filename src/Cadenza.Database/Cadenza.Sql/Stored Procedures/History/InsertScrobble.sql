CREATE PROCEDURE [History].[InsertScrobble]
	@TrackId INT,
	@ScrobbledAt DATETIME
AS
BEGIN

	DECLARE @ScrobbleId INT

	BEGIN TRANSACTION

		INSERT INTO [History].[Scrobbles] (
			[ScrobbledAt],
			[Track],
			[Artist],
			[Album],
			[AlbumArtist])
		SELECT
			@ScrobbledAt,
			TRK.[Title],
			ART.[Name],
			ALB.[Title],
			ALA.[Name]
		FROM 
			[Library].[Tracks] TRK
		INNER JOIN
			[Library].[Artists] ART ON ART.[Id] = TRK.[ArtistId]
		INNER JOIN
			[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
		INNER JOIN
			[Library].[Albums] ALB ON ALB.[Id] = DSC.[AlbumId]
		INNER JOIN
			[Library].[Artists] ALA ON ALA.[Id] = ALB.[ArtistId]
		WHERE
			TRK.[Id] = @TrackId

		SET @ScrobbleId = SCOPE_IDENTITY()

		INSERT INTO [History].[TrackScrobbles] (
			[ScrobbleId], 
			[TrackId])
		VALUES (
			@ScrobbleId, 
			@TrackId)

		EXECUTE [LastFm].[QueueScrobble] @ScrobbleId

	COMMIT TRANSACTION

END