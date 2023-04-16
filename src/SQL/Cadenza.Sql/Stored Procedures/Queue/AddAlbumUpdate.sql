CREATE PROCEDURE [Queue].[AddAlbumUpdate]
	@AlbumId INT,
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
		[Admin].[AlbumProperties]
	WHERE
		[Name] = @PropertyName

	SELECT
		@SourceId = [SourceId]
	FROM
		[Library].[Albums]
	WHERE
		[Id] = @AlbumId

	UPDATE
		[Queue].[AlbumUpdates]
	SET
		[DateRemoved] = GETDATE()
	WHERE
		[AlbumId] = @AlbumId
	AND
		[PropertyId] = @PropertyId
	AND
		[DateProcessed] IS NULL
	AND
		[DateRemoved] IS NULL

	INSERT INTO [Queue].[AlbumUpdates] (
		[AlbumId],
		[SourceId],
		[PropertyId],
		[OriginalValue],
		[UpdatedValue]
	)
	VALUES (
		@AlbumId,
		@SourceId,
		@PropertyId,
		@OriginalValue,
		@UpdatedValue
	)

END