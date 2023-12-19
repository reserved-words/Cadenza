CREATE PROCEDURE [Play].[GetGroupingTrackIds]
	@Grouping NVARCHAR(50),
	@LogRequest BIT
AS
BEGIN

	IF @LogRequest = 1
	BEGIN
		EXECUTE [Play].[LogGroupingRequest] @Grouping
	END

	SELECT 
		TRK.[Id]
	FROM
		[Library].[Tracks] TRK
	INNER JOIN 
		[Library].[Artists] ART ON ART.[Id] = TRK.[ArtistId]
	WHERE
		ART.[Grouping] = @Grouping

END