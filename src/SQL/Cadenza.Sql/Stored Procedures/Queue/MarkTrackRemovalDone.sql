CREATE PROCEDURE [Queue].[MarkTrackRemovalDone]
	@Id INT
AS
BEGIN

	UPDATE
		[Queue].[TrackRemovals]
	SET
		[DateProcessed] = GETDATE()
	WHERE
		[Id] = @Id

END