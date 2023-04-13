CREATE PROCEDURE [Queue].[AddTrackUpdate]
	@TrackIdFromSource NVARCHAR(500),
	@SourceId INT,
	@PropertyName NVARCHAR(50),
	@OriginalValue NVARCHAR(MAX),
	@UpdatedValue NVARCHAR(MAX)
AS
BEGIN

	DECLARE @PropertyId INT,
			@TrackId INT

	SELECT 
		@PropertyId = [Id] 
	FROM
		[Admin].[TrackProperties]
	WHERE
		[Name] = @PropertyName

	SELECT
		@TrackId = [Id]
	FROM
		[Library].[Tracks]
	WHERE
		[IdFromSource] = @TrackIdFromSource

	UPDATE
		[Queue].[TrackUpdates]
	SET
		[DateRemoved] = GETDATE()
	WHERE
		[TrackId] = @TrackId
	AND
		[PropertyId] = @PropertyId
	AND
		[DateProcessed] IS NULL
	AND
		[DateRemoved] IS NULL

	INSERT INTO [Queue].[TrackUpdates] (
		[TrackId],
		[SourceId],
		[PropertyId],
		[OriginalValue],
		[UpdatedValue]
	)
	VALUES (
		@TrackId,
		@SourceId,
		@PropertyId,
		@OriginalValue,
		@UpdatedValue
	)

END