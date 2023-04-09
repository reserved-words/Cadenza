CREATE PROCEDURE [Queue].[MarkArtistUpdateErrored]
	@Id INT
AS
BEGIN

	UPDATE
		[Queue].[ArtistUpdates]
	SET
		[DateErrored] = GETDATE()
	WHERE
		[Id] = @Id

END