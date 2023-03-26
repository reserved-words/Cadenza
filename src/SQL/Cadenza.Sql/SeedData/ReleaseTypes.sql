
DECLARE @ReleaseCategories TABLE ([Id] INT, [Name] NVARCHAR(50))
INSERT INTO @ReleaseCategories ([Id], [Name])
VALUES 
	(1, 'Studio Albums'),
	(2, 'Compilations'),
	(3, 'EPs & Singles'),
	(4, 'Other Releases'),
	(5, 'Various Artists'),
	(6, 'Playlists')

DECLARE @ReleaseTypes TABLE ([Id] INT, [Name] NVARCHAR(50), [ReleaseCategoryId] INT)
INSERT INTO @ReleaseTypes ([Id], [Name], [ReleaseCategoryId])
VALUES
	(1, 'Album', 1),
	(2, 'Best Of', 2),
	(3, 'Live', 2),
	(4, 'Compilation', 2),
	(5, 'EP', 3),
	(6, 'Single', 3),
	(7, 'Other', 4),
	(8, 'Various Artists', 5)

DECLARE @ReleaseTypeId INT,
		@ReleaseTypeName NVARCHAR(50),
		@ReleaseCategoryId INT,
		@ReleaseCategoryName NVARCHAR(50)

WHILE EXISTS (SELECT [Id] FROM @ReleaseCategories)
BEGIN
	SELECT @ReleaseCategoryId = [Id], @ReleaseCategoryName = [Name] FROM @ReleaseCategories

	IF EXISTS (SELECT [Id] FROM [Admin].[ReleaseCategories] WHERE [Id] = @ReleaseCategoryId)
	BEGIN
		UPDATE [Admin].[ReleaseCategories] SET [Name] = @ReleaseCategoryName WHERE [Id] = @ReleaseCategoryId
	END
	ELSE
	BEGIN
		INSERT INTO [Admin].[ReleaseCategories] ([Id], [Name]) VALUES (@ReleaseCategoryId, @ReleaseCategoryName)
	END
	
	DELETE FROM @ReleaseCategories WHERE [Id] = @ReleaseCategoryId
END

WHILE EXISTS (SELECT [Id] FROM @ReleaseTypes)
BEGIN
	SELECT @ReleaseTypeId = [Id], @ReleaseTypeName = [Name], @ReleaseCategoryId = [ReleaseCategoryId] FROM @ReleaseTypes

	IF EXISTS (SELECT [Id] FROM [Admin].[ReleaseTypes] WHERE [Id] = @ReleaseTypeId)
	BEGIN
		UPDATE [Admin].[ReleaseTypes] SET [Name] = @ReleaseTypeName, [ReleaseCategoryId] = @ReleaseCategoryId WHERE [Id] = @ReleaseTypeId
	END
	ELSE
	BEGIN
		INSERT INTO [Admin].[ReleaseTypes] ([Id], [Name], [ReleaseCategoryId]) VALUES (@ReleaseTypeId, @ReleaseTypeName, @ReleaseCategoryId)
	END
	
	DELETE FROM @ReleaseTypes WHERE [Id] = @ReleaseTypeId
END