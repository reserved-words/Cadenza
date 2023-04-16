CREATE PROCEDURE [Queue].[AddArtistUpdate]
	@ArtistId INT,
	@PropertyName NVARCHAR(50),
	@OriginalValue NVARCHAR(MAX),
	@UpdatedValue NVARCHAR(MAX)
AS
BEGIN

	DECLARE @PropertyId INT

	SELECT 
		@PropertyId = [Id] 
	FROM
		[Admin].[ArtistProperties]
	WHERE
		[Name] = @PropertyName

	UPDATE
		[Queue].[ArtistUpdates]
	SET
		[DateRemoved] = GETDATE()
	WHERE
		[ArtistId] = @ArtistId
	AND
		[PropertyId] = @PropertyId
	AND
		[DateProcessed] IS NULL
	AND
		[DateRemoved] IS NULL

	DECLARE @SourceId INT
	DECLARE @Sources TABLE ([SourceId] INT)
	INSERT INTO @Sources 
	SELECT [Id]
	FROM [Admin].[Sources]

	WHILE EXISTS (SELECT [SourceId] FROM @Sources)
	BEGIN

		SELECT @SourceId = [SourceId] FROM @Sources
		
		INSERT INTO [Queue].[ArtistUpdates] 
		(
			[ArtistId],
			[SourceId],
			[PropertyId],
			[OriginalValue],
			[UpdatedValue]
		)
		VALUES (
			@ArtistId,
			@SourceId,
			@PropertyId,
			@OriginalValue,
			@UpdatedValue
		)

		DELETE @Sources WHERE [SourceId] = @SourceId

	END

END