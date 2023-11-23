CREATE PROCEDURE [Update].[DeleteEmptyDiscs]
AS
BEGIN

	DELETE
		DSC
	FROM
		[Library].[Discs] DSC
	INNER JOIN 
		[Library].[vw_EmptyDiscs] EMP ON EMP.[Id] = DSC.[Id]

END