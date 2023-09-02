CREATE PROCEDURE [Library].[AddArtist]
	@Name NVARCHAR(200),
	@CompareName NVARCHAR(200),
	@GroupingName NVARCHAR(50),
	@Genre NVARCHAR(100),
	@City NVARCHAR(100),
	@State NVARCHAR(100),
	@Country NVARCHAR(100),
	@TagList NVARCHAR(1000),
	@ImageMimeType NVARCHAR(30),
	@ImageContent VARBINARY(MAX),
	@Id INT OUTPUT
AS
BEGIN

	SELECT @Id = [Id] 
	FROM [Library].[Artists] 
	WHERE [CompareName] = @CompareName

	IF @Id IS NOT NULL
		RETURN

	DECLARE @GroupingId INT

	SELECT @GroupingId = [Id]
	FROM [Admin].[Groupings]
	WHERE [Name] = @GroupingName

	INSERT INTO [Library].[Artists] (
		[Name],
		[CompareName],
		[GroupingId],
		[Genre],
		[City],
		[State],
		[Country]
	)
	VALUES (
		@Name,
		@CompareName,
		ISNULL(@GroupingId, 0),
		@Genre,
		@City,
		@State,
		@Country
	)

	SET @Id = SCOPE_IDENTITY()

	IF @ImageMimeType IS NOT NULL AND @ImageContent IS NOT NULL
	BEGIN
		INSERT INTO [Library].[ArtistImages] (
			[ArtistId],
			[MimeType],
			[Content]
		)
		VALUES (
			@Id, 
			@ImageMimeType,
			@ImageContent)
	END

	IF @TagList IS NOT NULL
	BEGIN

		INSERT INTO [Library].[ArtistTags] (
			[ArtistId],
			[Tag]
		)
		SELECT @Id, value 
		FROM STRING_SPLIT(@TagList, '|')
		WHERE RTRIM(value) <> ''

	END

END