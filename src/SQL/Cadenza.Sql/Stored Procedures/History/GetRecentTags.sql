CREATE PROCEDURE [History].[GetRecentTags]
	@MaxItems INT
AS
BEGIN

	SELECT 
		PLT.[Tag]
	FROM
		[History].[vw_PlayedTags] PLT
	INNER JOIN 
		[Library].[vw_UsedTags] UST ON UST.[Tag] = PLT.[Tag]
	ORDER BY
		PLT.[PlayedAt] DESC

END

