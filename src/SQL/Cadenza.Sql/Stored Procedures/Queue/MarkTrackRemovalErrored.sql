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

	UPDATE
		[Queue].[ArchivedTrackRemovals]
	SET
		[DateErrored] = GETDATE()
	WHERE
		[RequestId] = @Id

END