CREATE PROCEDURE [Library].[GetAlbumDiscs]
	@Id INT
AS
BEGIN

	SELECT 
		[DiscNo],
		[TrackCount]
	FROM
		[Library].[Discs]
	WHERE
		[AlbumId] = @Id

END