CREATE PROCEDURE [Queue].[MarkTrackUpdateErrored]
	@Id INT
AS
BEGIN

	UPDATE
		[Queue].[TrackUpdates]
	SET
		[DateErrored] = GETDATE()
	WHERE
		[Id] = @Id

END