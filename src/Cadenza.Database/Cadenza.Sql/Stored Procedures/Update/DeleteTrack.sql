CREATE PROCEDURE [Update].[DeleteTrack]
	@Id INT,
	@IdFromSource NVARCHAR(500)
AS
BEGIN

	IF (@Id IS NULL)
	BEGIN
		SELECT 
			@Id = [Id]
		FROM
			[Library].[Tracks]
		WHERE
			[IdFromSource] = @IdFromSource
	END

	DELETE
		[History].[TrackScrobbles]
	WHERE
		[TrackId] = @Id

	DELETE
		[History].[NowPlaying]
	WHERE
		[TrackId] = @Id

	DELETE
		[History].[NowPlaying]
	WHERE
		[TrackId] = @Id

	DELETE
		[LastFm].[LovedTracks]
	WHERE
		[TrackId] = @Id

	DELETE
		[Queue].[TrackUpdates]
	WHERE
		[TrackId] = @Id

	DELETE
		[Queue].[TrackUpdates]
	WHERE
		[TrackId] = @Id

	DELETE
		[Library].[TrackTags]
	WHERE
		[TrackId] = @Id

	DELETE 
		[Library].[Tracks]
	WHERE 
		[Id] = @Id

END