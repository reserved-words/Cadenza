CREATE PROCEDURE [History].[LogLibraryPlay]
AS
BEGIN
	
	INSERT INTO [History].[PlayedItems] (
		[PlaylistTypeId])
	SELECT 
		[Id]
	FROM
		[Admin].[PlaylistTypes]
	WHERE
		[Name] = 'All'

END