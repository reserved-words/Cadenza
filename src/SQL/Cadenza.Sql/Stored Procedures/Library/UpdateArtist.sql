CREATE PROCEDURE [Library].[UpdateArtist]
	@Id INT,
	@NameId NVARCHAR(200),
	@Name NVARCHAR(200),
	@GroupingId INT,
	@Genre NVARCHAR(100),
	@City NVARCHAR(100),
	@State NVARCHAR(100),
	@Country NVARCHAR(100),
	@Image NVARCHAR(MAX),
	@TagList NVARCHAR(1000)
AS
BEGIN

	UPDATE
		[Library].[Artists] 
	SET
		[NameId] = @NameId,
		[Name] = @Name,
		[GroupingId] = @GroupingId,
		[Genre] = @Genre,
		[City] = @City,
		[State] = @State,
		[Country] = @Country
	WHERE	
		[Id] = @Id

	DELETE  
		[Library].[ArtistImages]
	WHERE 
		[ArtistId] = @Id

	IF @Image IS NOT NULL
	BEGIN
		INSERT INTO [Library].[ArtistImages] (
			[ArtistId],
			[Image]
		)
		VALUES (@Id, @Image)
	END

	DELETE  
		[Library].[ArtistTags]
	WHERE 
		[ArtistId] = @Id


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