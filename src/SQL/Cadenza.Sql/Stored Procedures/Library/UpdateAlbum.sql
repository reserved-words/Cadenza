CREATE PROCEDURE [Library].[UpdateAlbum]
	@Id INT,
	@SourceId INT,
	@ArtistId INT,
	@Title NVARCHAR(500),
	@ReleaseTypeId INT,
	@Year NCHAR(4),
	@DiscCount INT,
	@TagList NVARCHAR(1000),
	@ArtworkMimeType NVARCHAR(30),
	@ArtworkContent VARBINARY(MAX)
AS
BEGIN

	UPDATE 
		[Library].[Albums] 
	SET
		[SourceId] = @SourceId, 
		[ArtistId] = @ArtistId, 
		[Title] = @Title,
		[ReleaseTypeId] = @ReleaseTypeId,
		[Year] = @Year,
		[DiscCount] = @DiscCount
	WHERE
		[Id] = @Id

	DELETE  
		[Library].[AlbumArtwork]
	WHERE 
		[AlbumId] = @Id

	IF @ArtworkMimeType IS NOT NULL AND @ArtworkContent IS NOT NULL
	BEGIN
		INSERT INTO [Library].[AlbumArtwork] (
			[AlbumId],
			[MimeType],
			[Content]
		)
		VALUES (
			@Id, 
			@ArtworkMimeType, 
			@ArtworkContent)
	END

	DELETE  
		[Library].[AlbumTags]
	WHERE 
		[AlbumId] = @Id

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