CREATE PROCEDURE [Queue].[MarkAlbumUpdateErrored]
	@Id INT
AS
BEGIN

	UPDATE
		[Queue].[AlbumUpdates]
	SET
		[DateErrored] = GETDATE()
	WHERE
		[Id] = @Id

END