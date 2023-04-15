CREATE PROCEDURE [Queue].[AddTrackRemoval]
	@TrackIdFromSource NVARCHAR(500)
AS
BEGIN

	DECLARE @TrackId INT,
			@SourceId INT

	SELECT
		@TrackId = TRK.[Id],
		@SourceId = ALB.[SourceId]
	FROM
		[Library].[Tracks] TRK
	INNER JOIN 
		[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
	INNER JOIN
		[Library].[Albums] ALB ON ALB.[Id] = DSC.[AlbumId]
	WHERE
		TRK.[IdFromSource] = @TrackIdFromSource

	UPDATE
		[Queue].[TrackRemovals]
	SET
		[DateRemoved] = GETDATE()
	WHERE
		[TrackId] = @TrackId
	AND
		[DateProcessed] IS NULL
	AND
		[DateRemoved] IS NULL

	INSERT INTO [Queue].[TrackRemovals] (
		[TrackId],
		[SourceId]
	)
	VALUES (
		@TrackId,
		@SourceId
	)

END