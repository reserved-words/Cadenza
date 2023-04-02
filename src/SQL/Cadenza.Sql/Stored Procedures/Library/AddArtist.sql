CREATE PROCEDURE [Library].[AddArtist]
	@NameId NVARCHAR(200),
	@Name NVARCHAR(200),
	@GroupingId INT,
	@Genre NVARCHAR(100),
	@City NVARCHAR(100),
	@State NVARCHAR(100),
	@Country NVARCHAR(100),
	@TagList NVARCHAR(1000),
	@Id INT OUTPUT
AS
BEGIN

	SELECT @Id = [Id] 
	FROM [Library].[Artists] 
	WHERE [NameId] = @NameId

	IF @Id IS NOT NULL
		RETURN

	INSERT INTO [Library].[Artists] (
		[NameId],
		[Name],
		[GroupingId],
		[Genre],
		[City],
		[State],
		[Country]
	)
	VALUES (
		@NameId,
		@Name,
		@GroupingId,
		@Genre,
		@City,
		@State,
		@Country
	)

	SET @Id = SCOPE_IDENTITY()

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