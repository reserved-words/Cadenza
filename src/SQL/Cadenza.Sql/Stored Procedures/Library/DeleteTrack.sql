CREATE PROCEDURE [Library].[DeleteTrack]
	@IdFromSource NVARCHAR(500)
AS
BEGIN

	DECLARE @Id INT

	SELECT 
		@Id = [Id]
	FROM
		[Library].[Tracks]
	WHERE 
		[IdFromSource] = @IdFromSource
		
	DELETE
		[Queue].[TrackUpdates]
	WHERE
		[TrackId] = @Id

	DELETE
		[Queue].[TrackUpdates]
	WHERE
		[TrackId] = @Id

	DELETE
		[Library].[TrackTags]
	WHERE
		[TrackId] = @Id

	DELETE 
		[Library].[Tracks]
	WHERE 
		[Id] = @Id

END