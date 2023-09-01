
IF NOT EXISTS (SELECT [Id] FROM [Admin].[Groupings])
BEGIN

	INSERT INTO [Admin].[Groupings] ([Id], [Name])
	VALUES (0, 'None')

END
GO