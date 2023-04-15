CREATE PROCEDURE [Queue].[MarkTrackRemovalErrored]
	@Id INT
AS
BEGIN

	UPDATE
		[Queue].[TrackRemovals]
	SET
		[DateErrored] = GETDATE()
	WHERE
		[Id] = @Id

END