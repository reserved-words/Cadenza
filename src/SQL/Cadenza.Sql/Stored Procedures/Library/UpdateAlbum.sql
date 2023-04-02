CREATE PROCEDURE [Library].[UpdateAlbum]
	@Id INT,
	@SourceId INT,
	@ArtistId INT,
	@Title NVARCHAR(500),
	@ReleaseTypeId INT,
	@Year NCHAR(4),
	@DiscCount INT,
	@TagList NVARCHAR(1000)
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