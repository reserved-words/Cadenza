CREATE FUNCTION [History].[GetStartDate]
(
	@HistoryPeriod INT
)
RETURNS DATETIME
AS
BEGIN
	
	--   Overall = 0,
	--   Week = 1,
	--   Month = 2,
	--   QuarterYear = 3,
	--   HalfYear = 4,
	--   Year = 5

	DECLARE @StartTime DATETIME,
			@CurrentTime DATETIME = GETDATE()

	SET @StartTime = CASE @HistoryPeriod
		WHEN 1 THEN DATEADD(WEEK, -1, @CurrentTime)
		WHEN 2 THEN DATEADD(MONTH, -1, @CurrentTime)
		WHEN 3 THEN DATEADD(MONTH, -3, @CurrentTime)
		WHEN 4 THEN DATEADD(MONTH, -6, @CurrentTime)
		WHEN 5 THEN DATEADD(YEAR, -1, @CurrentTime)
		ELSE NULL
	END

	RETURN @StartTime

END
