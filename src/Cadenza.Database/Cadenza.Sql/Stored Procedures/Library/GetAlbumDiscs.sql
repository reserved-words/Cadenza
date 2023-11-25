CREATE PROCEDURE [Library].[GetAlbumDiscs]
	@Id INT
AS
BEGIN

	SELECT 
		[Index] [DiscNo],
		[TrackCount]
	FROM
		[Library].[Discs]
	WHERE
		[AlbumId] = @Id

END