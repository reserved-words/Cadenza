CREATE PROCEDURE [Library].[GetTrackSourceId]
	@TrackId INT,
	@SourceId INT OUTPUT
AS
BEGIN

	SELECT 
		@SourceId = ALB.[SourceId]
	FROM
		[Library].[Tracks] TRK
	INNER JOIN 
		[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
	INNER JOIN 
		[Library].[Albums] ALB ON ALB.[Id] = DSC.[AlbumId]
	WHERE
		TRK.[Id] = @TrackId

END
