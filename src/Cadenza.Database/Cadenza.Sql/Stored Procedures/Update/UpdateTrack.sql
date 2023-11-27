CREATE PROCEDURE [Update].[UpdateTrack]
	@Id INT,
	@DiscNo INT,
	@TrackNo INT,
	@Title NVARCHAR(500),
	@Year NCHAR(4),
	@Lyrics NVARCHAR(MAX),
	@TagList NVARCHAR(1000)
AS
BEGIN

	EXECUTE [Update].[UpdateTrackDisc] @Id, @DiscNo

	UPDATE 
		[Library].[Tracks]
	SET
		[TrackNo] = @TrackNo,
		[Title] = @Title,
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

	EXECUTE [History].[UpdateTrackScrobbles] @Id, @Title

END