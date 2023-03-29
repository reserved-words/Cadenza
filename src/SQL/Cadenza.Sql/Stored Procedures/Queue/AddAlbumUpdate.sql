CREATE PROCEDURE [Queue].[AddAlbumUpdate]
	@AlbumId INT,
	@PropertyName NVARCHAR(50),
	@OriginalValue NVARCHAR(MAX),
	@UpdatedValue NVARCHAR(MAX)
AS
BEGIN

	DECLARE @PropertyId INT

	SELECT 
		@PropertyId = [Id] 
	FROM
		[Admin].[AlbumProperties]
	WHERE
		[Name] = @PropertyName

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
		[PropertyId],
		[OriginalValue],
		[UpdatedValue]
	)
	VALUES (
		@AlbumId,
		@PropertyId,
		@OriginalValue,
		@UpdatedValue
	)

END