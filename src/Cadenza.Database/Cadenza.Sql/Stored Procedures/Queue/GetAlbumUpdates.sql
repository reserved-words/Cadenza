CREATE PROCEDURE [Queue].[GetAlbumUpdates]
	@SourceId INT
AS
BEGIN

	SELECT 
		SNC.[AlbumId],
		ALB.[Title],
		REL.[Name] [ReleaseType],
		ALB.[Year],
		ALB.[DiscCount],
		IMG.[MimeType] [ArtworkMimeType],
		IMG.[Content] [ArtworkContent],
		TAG.[TagList]
	FROM
		[Queue].[AlbumSync] SNC
	INNER JOIN
		[Library].[Albums] ALB ON SNC.[AlbumId] = ALB.[Id]
	INNER JOIN
		[Admin].[ReleaseTypes] REL ON REL.[Id] = ALB.[ReleaseTypeId]
	LEFT JOIN
		[Library].[vw_AlbumTags] TAG ON TAG.[AlbumId] = ALB.[Id]
	LEFT JOIN
		[Library].[AlbumArtwork] IMG ON IMG.[AlbumId] = ALB.[Id]
	WHERE
		ALB.[SourceId] = @SourceId
	AND
		SNC.[LastSynced] < SNC.[LastUpdated]
	AND
		SNC.[FailedAttempts] < 3

END