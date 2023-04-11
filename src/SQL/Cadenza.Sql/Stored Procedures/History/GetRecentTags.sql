CREATE PROCEDURE [History].[GetRecentTags]
	@MaxItems INT
AS
BEGIN

	SELECT TOP (@MaxItems)
		[Tag]
	FROM
		[History].[vw_PlayedTags]
	ORDER BY
		[PlayedAt] DESC

END

