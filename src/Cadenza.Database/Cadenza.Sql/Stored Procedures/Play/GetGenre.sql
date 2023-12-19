CREATE PROCEDURE [Play].[GetGenre]
	@Grouping NVARCHAR(50),
	@Genre NVARCHAR(100)
AS
BEGIN

	DECLARE @IsUniqueGenre BIT = 1

	IF EXISTS (
		SELECT 
			[Id]
		FROM 
			[Library].[Artists]
		WHERE
			[Genre] = @Genre
		AND
			[Grouping] != @Grouping
	)
	BEGIN
		SET @IsUniqueGenre = 0
	END

	SELECT 
		@Grouping [Grouping], 
		@Genre [Genre], 
		@IsUniqueGenre [IsUniqueGenre]

END