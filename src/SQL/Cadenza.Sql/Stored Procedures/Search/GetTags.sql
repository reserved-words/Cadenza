CREATE PROCEDURE [Search].[GetTags]
AS
BEGIN

	SELECT 
		[Tag]
	FROM
		[Library].[vw_UsedTags]

END