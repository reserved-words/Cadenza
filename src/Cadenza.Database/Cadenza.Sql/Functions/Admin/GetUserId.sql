CREATE FUNCTION [Admin].[GetUserId]
(
	@Username NVARCHAR(100)
)
RETURNS INT
AS
BEGIN
	
	DECLARE @UserId INT

	SELECT 
		@UserId = [Id]
	FROM
		[Admin].[Users]
	WHERE
		[Username] = @Username

	RETURN @UserId

END
