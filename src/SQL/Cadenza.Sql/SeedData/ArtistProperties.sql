
DECLARE @ArtistProperties TABLE ([Id] INT, [Name] NVARCHAR(50))
INSERT INTO @ArtistProperties ([Id], [Name])
VALUES 
	(1, 'Grouping'),
	(2, 'Genre'),
	(3, 'Country'),
	(4, 'State'),
	(5, 'City'),
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