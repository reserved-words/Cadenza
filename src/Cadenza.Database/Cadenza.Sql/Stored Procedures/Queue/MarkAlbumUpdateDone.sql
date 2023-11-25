CREATE PROCEDURE [Queue].[MarkAlbumUpdateDone]
	@Id INT
AS
BEGIN

	UPDATE
		[Queue].[AlbumUpdates]
	SET
		[DateProcessed] = GETDATE()
	WHERE
		[Id] = @Id

END