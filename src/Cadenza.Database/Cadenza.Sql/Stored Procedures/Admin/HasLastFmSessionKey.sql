CREATE PROCEDURE [Admin].[HasLastFmSessionKey]
	@HasSessionKey BIT OUTPUT
AS
BEGIN

	SELECT @HasSessionKey = 
		CASE 
			WHEN [LastFmSessionKey] IS NOT NULL THEN 1
			ELSE 0
		END
	FROM
		[Admin].[User]

END