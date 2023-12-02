CREATE PROCEDURE [Admin].[HasLastFmSessionKey]
	@Username NVARCHAR(100),
	@HasSessionKey BIT OUTPUT
AS
BEGIN

	SELECT @HasSessionKey = 
		CASE 
			WHEN [LastFmSessionKey] IS NOT NULL THEN 1
			ELSE 0
		END
	FROM
		[Admin].[Users]
	WHERE
		[Username] = @Username

END