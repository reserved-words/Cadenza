CREATE PROCEDURE [Library].[GetTrack]
	@IdFromSource NVARCHAR(500)
AS
BEGIN

	SELECT 
		TRK.[Id],
		TRK.[IdFromSource],
		TRK.[ArtistId],
		TRK.[DiscId],
		TRK.[TrackNo],
		TRK.[Title],
		TRK.[DurationSeconds],
		TRK.[Year],
		TRK.[Lyrics],
		TAG.[TagList]
	FROM
		[Library].[Tracks] TRK
	LEFT JOIN
		[Library].[vw_TrackTags] TAG ON TAG.[TrackId] = TRK.[Id]
	WHERE
		TRK.[IdFromSource] = @IdFromSource

END