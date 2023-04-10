
DECLARE @PlaylistTypes TABLE ([Id] INT, [Name] NVARCHAR(50))
INSERT INTO @PlaylistTypes ([Id], [Name])
VALUES 
	(0, 'All'),
	(1, 'Artist'),
	(2, 'Album'),
	(3, 'Track'),
	(4, 'Genre'),
	(5, 'Grouping'),
	(6, 'Tag')

DECLARE @PlaylistTypeId INT,
		@PlaylistTypeName NVARCHAR(50)

WHILE EXISTS (SELECT [Id] FROM @PlaylistTypes)
BEGIN
	SELECT @PlaylistTypeId = [Id], @PlaylistTypeName = [Name] FROM @PlaylistTypes

	IF EXISTS (SELECT [Id] FROM [Admin].[PlaylistTypes] WHERE [Id] = @PlaylistTypeId)
	BEGIN
		UPDATE [Admin].[PlaylistTypes] SET [Name] = @PlaylistTypeName WHERE [Id] = @PlaylistTypeId
	END
	ELSE
	BEGIN
		INSERT INTO [Admin].[PlaylistTypes] ([Id], [Name]) VALUES (@PlaylistTypeId, @PlaylistTypeName)
	END
	
	DELETE FROM @PlaylistTypes WHERE [Id] = @PlaylistTypeId
END