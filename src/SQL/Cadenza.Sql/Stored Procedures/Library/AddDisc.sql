CREATE PROCEDURE [Library].[AddDisc]
	@AlbumId INT,
	@Index INT,
	@TrackCount INT,
	@Id INT OUTPUT
AS
BEGIN

	SELECT @Id = [Id] 
	FROM [Library].[Discs] 
	WHERE [AlbumId] = @AlbumId
		AND [Index] = @Index

	IF @Id IS NOT NULL
		RETURN

	INSERT INTO [Library].[Discs] (
		[AlbumId],
		[Index],
		[TrackCount]
	)
	VALUES (
		@AlbumId,
		@Index,
		@TrackCount
	)

	SET @Id = SCOPE_IDENTITY()

END