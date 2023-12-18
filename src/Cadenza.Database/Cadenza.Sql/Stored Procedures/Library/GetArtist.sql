﻿CREATE PROCEDURE [Library].[GetArtist]
	@Id INT
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
		ART.[Id] = @Id

END