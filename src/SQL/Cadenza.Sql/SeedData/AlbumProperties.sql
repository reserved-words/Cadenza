﻿
DECLARE @AlbumProperties TABLE ([Id] INT, [Name] NVARCHAR(50))
INSERT INTO @AlbumProperties ([Id], [Name])
VALUES 
	(1, 'ReleaseType'),
	(2, 'ReleaseYear'),
	(3, 'Artwork'),
	(4, 'AlbumTags')

DECLARE @AlbumPropertyId INT,
		@AlbumPropertyName NVARCHAR(50)

WHILE EXISTS (SELECT [Id] FROM @AlbumProperties)
BEGIN
	SELECT @AlbumPropertyId = [Id], @AlbumPropertyName = [Name] FROM @AlbumProperties

	IF EXISTS (SELECT [Id] FROM [Admin].[AlbumProperties] WHERE [Id] = @AlbumPropertyId)
	BEGIN
		UPDATE [Admin].[AlbumProperties] SET [Name] = @AlbumPropertyName WHERE [Id] = @AlbumPropertyId
	END
	ELSE
	BEGIN
		INSERT INTO [Admin].[AlbumProperties] ([Id], [Name]) VALUES (@AlbumPropertyId, @AlbumPropertyName)
	END
	
	DELETE FROM @AlbumProperties WHERE [Id] = @AlbumPropertyId
END