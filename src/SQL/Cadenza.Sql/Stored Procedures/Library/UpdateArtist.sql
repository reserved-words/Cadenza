CREATE PROCEDURE [Library].[UpdateArtist]
	@Id INT,
	@NameId NVARCHAR(200),
	@Name NVARCHAR(200),
	@GroupingId INT,
	@Genre NVARCHAR(100),
	@City NVARCHAR(100),
	@State NVARCHAR(100),
	@Country NVARCHAR(100),
	@ImageUrl NVARCHAR(500),
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
		[Country] = @Country,
		[ImageUrl] = @ImageUrl
	WHERE	
		[Id] = @Id

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