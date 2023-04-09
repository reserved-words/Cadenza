
DECLARE @Groupings TABLE ([Id] INT, [Name] NVARCHAR(50))
INSERT INTO @Groupings ([Id], [Name])
VALUES 
	(0, 'None'),
	(1, 'Alternative'),
	(2, 'Classical'),
	(3, 'Metal'),
	(4, 'Musicals'),
	(5, 'Pop')

DECLARE @GroupingId INT,
		@GroupingName NVARCHAR(50)

WHILE EXISTS (SELECT [Id] FROM @Groupings)
BEGIN
	SELECT @GroupingId = [Id], @GroupingName = [Name] FROM @Groupings

	IF EXISTS (SELECT [Id] FROM [Admin].[Groupings] WHERE [Id] = @GroupingId)
	BEGIN
		UPDATE [Admin].[Groupings] SET [Name] = @GroupingName WHERE [Id] = @GroupingId
	END
	ELSE
	BEGIN
		INSERT INTO [Admin].[Groupings] ([Id], [Name]) VALUES (@GroupingId, @GroupingName)
	END
	
	DELETE FROM @Groupings WHERE [Id] = @GroupingId
END