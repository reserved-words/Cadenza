CREATE PROCEDURE [Library].[AddAlbum]
	@SourceId INT,
	@ArtistId INT,
	@Title NVARCHAR(500),
	@ReleaseTypeId INT,
	@Year NCHAR(4),
	@DiscCount INT,
	@Artwork NVARCHAR(MAX),
	@TagList NVARCHAR(1000),
	@Id INT OUTPUT
AS
BEGIN

	SELECT @Id = [Id] 
	FROM [Library].[Albums] 
	WHERE [ArtistId] = @ArtistId
		AND [Title] = @Title
		AND [ReleaseTypeId] = @ReleaseTypeId

	IF @Id IS NOT NULL
		RETURN

	INSERT INTO [Library].[Albums] (
		[SourceId], 
		[ArtistId], 
		[Title],
		[ReleaseTypeId],
		[Year],
		[DiscCount]
	)
	VALUES (
		@SourceId,
		@ArtistId,
		@Title,
		@ReleaseTypeId,
		@Year,
		@DiscCount
	)

	SET @Id = SCOPE_IDENTITY()

	IF @Artwork IS NOT NULL
	BEGIN
		INSERT INTO [Library].[AlbumArtwork] (
			[AlbumId],
			[Artwork]
		)
		VALUES (@Id, @Artwork)
	END

	IF @TagList IS NOT NULL
	BEGIN

		INSERT INTO [Library].[AlbumTags] (
			[AlbumId],
			[Tag]
		)
		SELECT @Id, value 
		FROM STRING_SPLIT(@TagList, '|')
		WHERE RTRIM(value) <> ''

	END

END