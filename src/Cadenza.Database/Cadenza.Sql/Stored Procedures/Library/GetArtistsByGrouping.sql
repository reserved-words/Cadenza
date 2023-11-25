CREATE PROCEDURE [Library].[GetArtistsByGrouping]
	@GroupingId NVARCHAR(100)
AS
BEGIN

	SELECT 
		ART.[Id],
		ART.[Name],
		ART.[GroupingId],
		GRP.[Name] [GroupingName],
		ART.[Genre]
	FROM 
		[Library].[Artists] ART
	INNER JOIN
		[Admin].[Groupings] GRP ON GRP.[Id] = ART.[GroupingId]
	WHERE
		ART.[GroupingId] = @GroupingId

END