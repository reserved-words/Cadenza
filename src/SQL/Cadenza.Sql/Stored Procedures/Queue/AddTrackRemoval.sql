CREATE PROCEDURE [Queue].[AddTrackRemoval]
	@TrackIdFromSource NVARCHAR(500),
	@SourceId INT
AS
BEGIN

	DECLARE @TrackId INT

	SELECT
		@TrackId = [Id]
	FROM
		[Library].[Tracks]
	WHERE
		[IdFromSource] = @TrackIdFromSource

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