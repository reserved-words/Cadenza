CREATE PROCEDURE [Queue].[MarkArtistUpdateDone]
	@Id INT
AS
BEGIN

	UPDATE
		[Queue].[ArtistUpdates]
	SET
		[DateProcessed] = GETDATE()
	WHERE
		[Id] = @Id

END