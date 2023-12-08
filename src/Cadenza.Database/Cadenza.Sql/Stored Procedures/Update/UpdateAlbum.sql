CREATE PROCEDURE [Update].[UpdateAlbum]
	@Id INT,
	@ArtistId INT,
	@Title NVARCHAR(500),
	@ReleaseTypeId INT,
	@DiscCount INT,
	@Year NCHAR(4),
	@TagList NVARCHAR(1000),
	@ArtworkMimeType NVARCHAR(30),
	@ArtworkContent VARBINARY(MAX)
AS
BEGIN

	IF EXISTS (
		SELECT 
			[Id]
		FROM 
			[Library].[Albums]
		WHERE 
			[ArtistId] = @ArtistId
			AND [Title] = @Title
			AND [ReleaseTypeId] = @ReleaseTypeId
			AND [Id] != @Id
	)
		THROW 51000, 'An album already exists with the same artist, title and release type', 1;

	UPDATE 
		[Library].[Albums] 
	SET
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

	EXECUTE [History].[UpdateAlbumScrobbles] @Id, @Title

END