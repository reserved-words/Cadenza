CREATE PROCEDURE [Library].[GetTrackIdFromSource]
	@Id INT
AS
BEGIN

	SELECT 
		[IdFromSource]
	FROM
		[Library].[Tracks]
	WHERE
		[Id] = @Id

END