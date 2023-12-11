CREATE PROCEDURE [Update].[AddTrack]
	@IdFromSource NVARCHAR(500),
	@ArtistId INT,
	@DiscId INT,
	@TrackNo INT,
	@Title NVARCHAR(500),
	@DurationSeconds INT,
	@Year NCHAR(4),
	@Lyrics NVARCHAR(MAX),
	@TagList NVARCHAR(1000),
	@Id INT OUTPUT
AS
BEGIN

	SELECT @Id = [Id] 
	FROM [Library].[Tracks] 
	WHERE [IdFromSource] = @IdFromSource

	IF @Id IS NOT NULL
		RETURN

	INSERT INTO [Library].[Tracks] (
		[IdFromSource],
		[ArtistId],
		[DiscId],
		[TrackNo],
		[Title],
		[DurationSeconds],
		[Year],
		[Lyrics]
	)
	VALUES (
		@IdFromSource,
		@ArtistId,
		@DiscId,
		@TrackNo,
		@Title,
		@DurationSeconds,
		@Year,
		@Lyrics
	)

	SET @Id = SCOPE_IDENTITY()

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