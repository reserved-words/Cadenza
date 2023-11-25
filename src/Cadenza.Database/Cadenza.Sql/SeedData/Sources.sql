
DECLARE @Sources TABLE ([Id] INT, [Name] NVARCHAR(50))
INSERT INTO @Sources ([Id], [Name])
VALUES (1, 'Local')

DECLARE @SourceId INT,
		@SourceName NVARCHAR(50)

WHILE EXISTS (SELECT [Id] FROM @Sources)
BEGIN
	SELECT @SourceId = [Id], @SourceName = [Name] FROM @Sources

	IF EXISTS (SELECT [Id] FROM [Admin].[Sources] WHERE [Id] = @SourceId)
	BEGIN
		UPDATE [Admin].[Sources] SET [Name] = @SourceName WHERE [Id] = @SourceId
	END
	ELSE
	BEGIN
		INSERT INTO [Admin].[Sources] ([Id], [Name]) VALUES (@SourceId, @SourceName)
	END
	
	DELETE FROM @Sources WHERE [Id] = @SourceId
END