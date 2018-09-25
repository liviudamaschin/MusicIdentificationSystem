IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[sp_TimeReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [sp_TimeReport]
GO

/*

DECLARE    
   @StartDate DATETIME, 
   @EndDate DATETIME,
	@AccountIds NVARCHAR(MAX) = NULL,
	@StreamStationIds NVARCHAR(MAX) = NULL,
	@TrackIds NVARCHAR(MAX) = NULL,
   @ReportType INT

   SET @StartDate = '20180320'
   SET @EndDate = '20180627'
	SET @AccountIds = '1,2,11'
	SET @StreamStationIds = '1,2,3,4,5,6,7,8,30,31,32,33,34,35,36,37,39,40,41,42,43,44,45'
	SET @TrackIds = '57,58,59,60,61,62,63,64,65,66,67,68'
   SET @ReportType = 3

   EXEC [sp_TimeReport]
      @StartDate, 
      @EndDate,
	   @AccountIds,
	   @StreamStationIds,
	   @TrackIds,
      @ReportType;


DECLARE    
   @StartDate DATETIME, 
   @EndDate DATETIME,
	@AccountIds NVARCHAR(MAX) = NULL,
	@StreamStationIds NVARCHAR(MAX) = NULL,
	@TrackIds NVARCHAR(MAX) = NULL,
   @ReportType INT

   SET @StartDate = '20180320'
   SET @EndDate = '20180627'
	SET @AccountIds = '1,2,11'
	SET @StreamStationIds = '1,2,3,4,5,6,7,8,30,31,32,33,34,35,36,37,39,40,41,42,43,44,45'
	SET @TrackIds = '57,58,59,60,61,62,63,64,65,66,67,68'
   SET @ReportType = 4

   EXEC [sp_TimeReport]
      @StartDate, 
      @EndDate,
	   @AccountIds,
	   @StreamStationIds,
	   @TrackIds,
      @ReportType;

*/


CREATE PROCEDURE [dbo].[sp_TimeReport]
(
   @StartDate DATETIME, 
   @EndDate DATETIME,
	@AccountIds NVARCHAR(MAX) = NULL,
	@StreamStationIds NVARCHAR(MAX) = NULL,
	@TrackIds NVARCHAR(MAX) = NULL,
   @ReportType INT -- 1 - fara group by, cu data start melodie (time report details)
                   -- 2 - cu group by  dupa account, stream station si track (time report first aggregation)
                   -- 3 - cu group by dupa account (time report maximum aggregation)
                   -- 4 - count cu group by  dupa account, stream station si track (count report first aggregation)
                   -- 5 - cu group by dupa account (count report maximum aggregation)
)

AS

BEGIN

   IF OBJECT_ID('tempdb..#tmpAccounts') IS NOT NULL
      DROP TABLE #tmpAccounts
   IF OBJECT_ID('tempdb..#tmpStreamStations') IS NOT NULL
      DROP TABLE #tmpStreamStations
   IF OBJECT_ID('tempdb..#tmpTracks') IS NOT NULL
      DROP TABLE #tmpTracks
   IF OBJECT_ID('tempdb..#tmpResults') IS NOT NULL
      DROP TABLE #tmpResults

   CREATE TABLE #tmpAccounts
   (
      Position BIGINT,
      TxtValue VARCHAR(MAX)
   )

   CREATE TABLE #tmpStreamStations
   (
      Position BIGINT,
      TxtValue VARCHAR(MAX)
   )

   CREATE TABLE #tmpTracks
   (
      Position BIGINT,
      TxtValue VARCHAR(MAX)
   )

	IF (@AccountIds IS NOT NULL) BEGIN
      INSERT #tmpAccounts
		SELECT Position, TxtValue 
		FROM [dbo].[fn_SplitText](@AccountIds, ',')  
	END
   ELSE BEGIN
      INSERT #tmpAccounts
      SELECT A.Id as Position, CONVERT(VARCHAR(MAX), A.Id) as TxtValue
      FROM Accounts A
      WHERE A.IsActive = 1
   END

	IF (@StreamStationIds IS NOT NULL) BEGIN
      INSERT #tmpStreamStations
		SELECT Position, TxtValue
		FROM [dbo].[fn_SplitText](@StreamStationIds, ',')  
	END
   ELSE BEGIN
      INSERT #tmpStreamStations
      SELECT SS.Id as Position, CONVERT(VARCHAR(MAX), SS.Id) as TxtValue
      FROM StreamStations SS
      WHERE SS.IsActive = 1
   END

	IF (@TrackIds IS NOT NULL) BEGIN
      INSERT #tmpTracks
		SELECT Position, TxtValue  
		FROM [dbo].[fn_SplitText](@TrackIds, ',')  
	END
   ELSE BEGIN
      INSERT #tmpTracks
      SELECT T.Id as Position, CONVERT(VARCHAR(MAX), T.Id) as TxtValue
      FROM Tracks T
      WHERE T.IsActive = 1
   END

   SELECT A.Id AS AccountId, A.AccountName, 
      SS.Id as StreamStationId, SS.StationName as StreamStationName, 
      T.Id as TrackId, T.Title, T.Artist, T.Length ,
      R.Id as ResultId, R.QueryMatchLength, R.QueryMatchStartsAt,
      S.Id as StreamId, S.FileName, S.StartTime, S.EndTime
   INTO #tmpResults
   FROM Results R
   JOIN Tracks T ON R.TrackId = T.Id
   JOIN Stream S ON R.StreamId = S.Id
   JOIN StreamStations SS ON S.StationId = SS.Id
   JOIN StreamStationXTracks SSXT ON SS.Id = SSXT.StreamStationId AND T.Id = SSXT.TrackId
   JOIN AccountXTracks AXT ON T.Id = AXT.TrackId
   JOIN Accounts A ON AXT.AccountId = A.Id
   JOIN #tmpAccounts tA ON A.Id = tA.TxtValue
   JOIN #tmpStreamStations tSS ON SS.Id = tSS.TxtValue
   JOIN #tmpTracks tT ON T.Id = tT.TxtValue
   WHERE
      S.StartTime <= @EndDate AND @StartDate <= S.EndTime

   IF (@ReportType = 1) BEGIN
      -- @ReportType = 1
      SELECT *
      FROM #tmpResults
   END
   ELSE IF (@ReportType = 2) BEGIN
      -- @ReportType = 2
      SELECT t.AccountId, t.AccountName, 
         t.StreamStationId, t.StreamStationName, 
         t.TrackId, T.Title, T.Artist, T.Length ,
         CAST(SUM(t.QueryMatchLength) as DECIMAL) as AccountResultsInSeconds, CAST(DATEDIFF(SECOND, @StartDate, @EndDate) as BIGINT) as TotalTimeInSeconds,
         CAST(SUM(t.QueryMatchLength) * 100 / DATEDIFF(SECOND, @StartDate, @EndDate) as DECIMAL) as AccountPercent
      FROM #tmpResults t
      GROUP BY t.AccountId, t.AccountName, t.StreamStationId, t.StreamStationName, t.TrackId, t.Title, t.Artist, t.Length
   END
   ELSE IF (@ReportType = 3) BEGIN
      -- @ReportType = 3
      SELECT t.AccountId, t.AccountName, 
         --t.StreamStationId, t.StreamStationName, 
         --t.TrackId, T.Title, T.Artist, T.Length ,
         CAST(SUM(t.QueryMatchLength) as DECIMAL) as AccountResultsInSeconds, CAST(DATEDIFF(SECOND, @StartDate, @EndDate) as BIGINT) as TotalTimeInSeconds,
         CAST(SUM(t.QueryMatchLength) * 100 / DATEDIFF(SECOND, @StartDate, @EndDate) as DECIMAL) as AccountPercent
      FROM #tmpResults t
      GROUP BY t.AccountId, t.AccountName --, t.StreamStationId, t.StreamStationName, t.TrackId, t.Title, t.Artist, t.Length
   END
   ELSE IF (@ReportType = 4) BEGIN
      -- @ReportType = 4
      SELECT t.AccountId, t.AccountName, 
         t.StreamStationId, t.StreamStationName, 
         t.TrackId, T.Title, T.Artist, T.Length ,
         CAST(COUNT(t.ResultId) as BIGINT) AS CountAccountResults,
         CAST(SUM(t.Length) as DECIMAL) as AccountResultsInSeconds, CAST(DATEDIFF(SECOND, @StartDate, @EndDate) as BIGINT) as TotalTimeInSeconds,
         CAST(SUM(t.Length) * 100 / DATEDIFF(SECOND, @StartDate, @EndDate) as DECIMAL) as AccountPercent
      FROM #tmpResults t
      GROUP BY t.AccountId, t.AccountName, t.StreamStationId, t.StreamStationName, t.TrackId, t.Title, t.Artist, t.Length
   END
   ELSE IF (@ReportType = 5) BEGIN
      -- @ReportType = 5
      SELECT t.AccountId, t.AccountName, 
         --t.StreamStationId, t.StreamStationName, 
         --t.TrackId, T.Title, T.Artist, T.Length ,
         CAST(COUNT(t.ResultId) as BIGINT) AS CountAccountResults,
         CAST(SUM(t.Length) as DECIMAL) as AccountResultsInSeconds, CAST(DATEDIFF(SECOND, @StartDate, @EndDate) as BIGINT) as TotalTimeInSeconds,
         CAST(SUM(t.Length) * 100 / DATEDIFF(SECOND, @StartDate, @EndDate) as DECIMAL) as AccountPercent
      FROM #tmpResults t
      GROUP BY t.AccountId, t.AccountName --, t.StreamStationId, t.StreamStationName, t.TrackId, t.Title, t.Artist, t.Length
   END
END

