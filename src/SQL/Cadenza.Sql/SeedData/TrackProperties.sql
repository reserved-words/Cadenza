
DECLARE @TrackProperties TABLE ([Id] INT, [Name] NVARCHAR(50))
INSERT INTO @TrackProperties ([Id], [Name])
VALUES 
	(1, 'TrackTitle'),
	(2, 'TrackYear'),
	(3, 'TrackLyrics'),
	(4, 'TrackTags'),
	(5, 'TrackNo')

DECLARE @TrackPropertyId INT,
		@TrackPropertyName NVARCHAR(50)

WHILE EXISTS (SELECT [Id] FROM @TrackProperties)
BEGIN
	SELECT @TrackPropertyId = [Id], @TrackPropertyName = [Name] FROM @TrackProperties

	IF EXISTS (SELECT [Id] FROM [Admin].[TrackProperties] WHERE [Id] = @TrackPropertyId)
	BEGIN
		UPDATE [Admin].[TrackProperties] SET [Name] = @TrackPropertyName WHERE [Id] = @TrackPropertyId
	END
	ELSE
	BEGIN
		INSERT INTO [Admin].[TrackProperties] ([Id], [Name]) VALUES (@TrackPropertyId, @TrackPropertyName)
	END
	
	DELETE FROM @TrackProperties WHERE [Id] = @TrackPropertyId
END