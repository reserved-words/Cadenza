CREATE PROCEDURE [Update].[GetTrackForUpdate]
	@Id INT
AS
BEGIN

	SELECT 
		TRK.[Id],
		TRK.[IdFromSource],
		TRK.[ArtistId],
		TRK.[DiscId],
		DSC.[DiscNo],
		TRK.[TrackNo],
		TRK.[Title],
		TRK.[DurationSeconds],
		TRK.[Year],
		TRK.[Lyrics],
		TAG.[TagList]
	FROM
		[Library].[Tracks] TRK
	INNER JOIN
		[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
	LEFT JOIN
		[Library].[vw_TrackTags] TAG ON TAG.[TrackId] = TRK.[Id]
	WHERE
		TRK.[Id] = @Id

END