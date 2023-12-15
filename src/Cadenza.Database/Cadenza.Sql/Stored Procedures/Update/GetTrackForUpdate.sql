CREATE PROCEDURE [Update].[GetTrackForUpdate]
	@Id INT
AS
BEGIN

	SELECT 
		DSC.[DiscNo],
		TRK.[TrackNo],
		TRK.[Title],
		TRK.[Year],
		TRK.[Lyrics],
		TAG.[TagList],
		DSC.[TrackCount] [DiscTrackCount]
	FROM
		[Library].[Tracks] TRK
	INNER JOIN
		[Library].[Discs] DSC ON DSC.[Id] = TRK.[DiscId]
	LEFT JOIN
		[Library].[vw_TrackTags] TAG ON TAG.[TrackId] = TRK.[Id]
	WHERE
		TRK.[Id] = @Id

END