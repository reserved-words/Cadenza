CREATE PROCEDURE [Queue].[AddTrackUpdate]
	@TrackId INT,
	@PropertyName NVARCHAR(50),
	@OriginalValue NVARCHAR(MAX),
	@UpdatedValue NVARCHAR(MAX)
AS
BEGIN

	DECLARE @PropertyId INT,
			@SourceId INT

	SELECT 
		@PropertyId = [Id] 
	FROM
		[Admin].[TrackProperties]
	WHERE
		[Name] = @PropertyName

	EXECUTE [Library].[GetTrackSourceId] @TrackId, @SourceId OUTPUT

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