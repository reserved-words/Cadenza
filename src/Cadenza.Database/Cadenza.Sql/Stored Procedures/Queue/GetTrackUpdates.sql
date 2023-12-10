CREATE PROCEDURE [Queue].[GetTrackUpdates]
	@SourceId INT
AS
BEGIN

	SELECT 
		SNC.[TrackId],
		TRK.[Title],
		TRK.[Year],
		TRK.[Lyrics],
		DSC.[DiscNo],
		TRK.[TrackNo],
		DSC.[TrackCount] [DiscTrackCount],
		TAG.[TagList]
	FROM
		[Queue].[TrackSync] SNC
	INNER JOIN
		[Library].[Tracks] TRK ON SNC.[TrackId] = TRK.[Id]
	INNER JOIN
		[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
	INNER JOIN
		[Library].[Albums] ALB ON ALB.[Id] = DSC.[AlbumId]
	LEFT JOIN
		[Library].[vw_TrackTags] TAG ON TAG.[TrackId] = TRK.[Id]
	WHERE
		ALB.[SourceId] = @SourceId
	AND
		(SNC.[LastSynced] IS NULL OR SNC.[LastSynced] < SNC.[LastUpdated])
	AND
		SNC.[FailedAttempts] < 3

END