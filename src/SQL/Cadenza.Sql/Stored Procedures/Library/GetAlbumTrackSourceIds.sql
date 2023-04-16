CREATE PROCEDURE [Library].[GetAlbumTrackSourceIds]
	@Id INT
AS
BEGIN

	SELECT 
		TRK.[IdFromSource]
	FROM
		[Library].[Tracks] TRK
	INNER JOIN
		[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
	WHERE
		DSC.[AlbumId] = @Id

END