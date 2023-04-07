CREATE PROCEDURE [Library].[UpdateAlbum]
	@Id INT,
	@SourceId INT,
	@ArtistId INT,
	@Title NVARCHAR(500),
	@ReleaseTypeId INT,
	@Year NCHAR(4),
	@DiscCount INT,
	@Artwork NVARCHAR(MAX),
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
		[Library].[AlbumArtwork]
	WHERE 
		[AlbumId] = @Id

	IF @Artwork IS NOT NULL
	BEGIN
		INSERT INTO [Library].[AlbumArtwork] (
			[AlbumId],
			[Artwork]
		)
		VALUES (@Id, @Artwork)
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