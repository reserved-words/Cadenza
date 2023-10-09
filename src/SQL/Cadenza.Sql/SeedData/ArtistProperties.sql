
DECLARE @ArtistProperties TABLE ([Id] INT, [Name] NVARCHAR(50))
INSERT INTO @ArtistProperties ([Id], [Name])
VALUES 
	(1, 'ArtistGrouping'),
	(2, 'ArtistGenre'),
	(3, 'ArtistCountry'),
	(4, 'ArtistState'),
	(5, 'ArtistCity'),
	(6, 'ArtistImage'),
	(7, 'ArtistTags')

DECLARE @ArtistPropertyId INT,
		@ArtistPropertyName NVARCHAR(50)

WHILE EXISTS (SELECT [Id] FROM @ArtistProperties)
BEGIN
	SELECT @ArtistPropertyId = [Id], @ArtistPropertyName = [Name] FROM @ArtistProperties

	IF EXISTS (SELECT [Id] FROM [Admin].[ArtistProperties] WHERE [Id] = @ArtistPropertyId)
	BEGIN
		UPDATE [Admin].[ArtistProperties] SET [Name] = @ArtistPropertyName WHERE [Id] = @ArtistPropertyId
	END
	ELSE
	BEGIN
		INSERT INTO [Admin].[ArtistProperties] ([Id], [Name]) VALUES (@ArtistPropertyId, @ArtistPropertyName)
	END
	
	DELETE FROM @ArtistProperties WHERE [Id] = @ArtistPropertyId
END