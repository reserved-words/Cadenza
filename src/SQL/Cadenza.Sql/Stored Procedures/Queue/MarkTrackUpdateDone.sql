CREATE PROCEDURE [Queue].[MarkTrackUpdateDone]
	@Id INT
AS
BEGIN

	UPDATE
		[Queue].[TrackUpdates]
	SET
		[DateProcessed] = GETDATE()
	WHERE
		[Id] = @Id

END