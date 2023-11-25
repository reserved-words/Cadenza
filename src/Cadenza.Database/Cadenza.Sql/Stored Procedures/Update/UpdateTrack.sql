CREATE PROCEDURE [Update].[UpdateTrack]
	@Id INT,
	@IdFromSource NVARCHAR(500),
	@ArtistId INT,
	@DiscId INT,
	@DiscNo INT,
	@TrackNo INT,
	@Title NVARCHAR(500),
	@DurationSeconds INT,
	@Year NCHAR(4),
	@Lyrics NVARCHAR(MAX),
	@TagList NVARCHAR(1000)
AS
BEGIN

	EXECUTE [Update].[UpdateTrackDisc] @Id, @DiscNo

	UPDATE 
		[Library].[Tracks]
	SET
		[IdFromSource] = @IdFromSource,
		[ArtistId] = @ArtistId,
		[TrackNo] = @TrackNo,
		[Title] = @Title,
		[DurationSeconds] = @DurationSeconds,
		[Year] = @Year,
		[Lyrics] = @Lyrics
	WHERE
		[Id] = @Id

	DELETE  
		[Library].[TrackTags]
	WHERE 
		[TrackId] = @Id

	IF @TagList IS NOT NULL
	BEGIN

		INSERT INTO [Library].[TrackTags] (
			[TrackId],
			[Tag]
		)
		SELECT @Id, value 
		FROM STRING_SPLIT(@TagList, '|')
		WHERE RTRIM(value) <> ''

	END

END