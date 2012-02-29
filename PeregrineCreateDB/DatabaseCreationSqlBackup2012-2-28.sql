USE [PeregrineDB]
GO
/****** Object:  Table [dbo].[Job]    Script Date: 02/28/2012 18:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Job](
	[JobID] [int] IDENTITY(1,1) NOT NULL,
	[JobName] [nvarchar](200) NOT NULL,
	[PlannedCount] [int] NULL,
	[CompletedCount] [int] NULL,
	[PercentComplete] [float] NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[JobID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetStartStopMessagesWithProcessID]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kyle P. / Weixiong Lu
-- Create date: 2012-02-04
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetStartStopMessagesWithProcessID] 
	-- Add the parameters for the stored procedure here
	(
		@ProcessID int,
		@SortBy int = 3,
		@Order int = 0,
		@From int = 0,
		@To int = 10
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

	DECLARE @SQLString nvarchar(700);
	DECLARE @Orderby nvarchar(100);
	DECLARE @Direction nvarchar(4);

	IF(@SortBy = 2) 
		SET @Orderby = 'dbo.Message.Message';
	ELSE
	IF(@SortBy = 3)
		SET @Orderby = 'dbo.Message.Date';
	ELSE
	IF(@SortBy = 4)
		SET @Orderby = 'dbo.Message.Category';
	ELSE
	IF(@SortBy = 5)
		SET @Orderby = 'dbo.Message.Priority';


	IF(@Order = 0) 
		SET @Direction = 'DESC';
	ELSE
		SET @Direction = 'ASC';


	SET @SQLString = 
	N'WITH NumberedRows AS
	(
	SELECT 
		dbo.Message.MessageID, dbo.Message.Message, dbo.Message.Date, dbo.Message.Category, dbo.Message.Priority,
		ROW_NUMBER() OVER (ORDER BY '+@Orderby+' '+@Direction+') AS RowNumber
	FROM dbo.LogRel
	LEFT JOIN dbo.Message on dbo.LogRel.MessageID = dbo.Message.MessageID
	WHERE (dbo.LogRel.ProcessID = '+CONVERT(varchar(5), @PRocessID)+' AND (dbo.Message.Category = 3 OR dbo.Message.Category = 4))
	)
	SELECT MessageID, Message, Date, Category, Priority
	FROM NumberedRows
	WHERE RowNumber BETWEEN '+CONVERT(varchar(5), @From)+' AND '+CONVERT(varchar(5), @To);

	/*
	WITH NumberedRows AS
	(
	SELECT 
		dbo.Message.MessageID, dbo.Message.Message, dbo.Message.Date, dbo.Message.Category, dbo.Message.Priority,
		ROW_NUMBER() OVER (ORDER BY dbo.Message.Category DESC) AS RowNumber
	FROM dbo.LogRel
	LEFT JOIN dbo.Message on dbo.LogRel.MessageID = dbo.Message.MessageID
	WHERE (dbo.LogRel.ProcessID = 1 AND (dbo.Message.Category = 3 OR dbo.Message.Category = 4))
	)
	SELECT MessageID, Message, Date, Category, Priority
	FROM NumberedRows
	WHERE RowNumber BETWEEN 0 AND 10;
	*/

	--EXECUTE sp_executesql @SQLString, N'@P_ID int', @P_ID = @ProcessID 
	EXECUTE sp_executesql @SQLString

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetPageOfMessages]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kyle Palsen / Weixiong Lu
-- Create date: 2012-02-04
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetPageOfMessages] 
	-- Add the parameters for the stored procedure here
	(
		@SortBy int = 3,
		@Order int = 0,
		@From int = 0,
		@To int = 10
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

	DECLARE @SQLString nvarchar(700);
	DECLARE @Orderby nvarchar(100);
	DECLARE @Direction nvarchar(4);

	IF(@SortBy = 2) 
		SET @Orderby = 'dbo.Message.Message';
	ELSE
	IF(@SortBy = 3)
		SET @Orderby = 'dbo.Message.Date';
	ELSE
	IF(@SortBy = 4)
		SET @Orderby = 'dbo.Message.Category';
	ELSE
	IF(@SortBy = 5)
		SET @Orderby = 'dbo.Message.Priority';


	IF(@Order = 0) 
		SET @Direction = 'DESC';
	ELSE
		SET @Direction = 'ASC';


	SET @SQLString = 
	N'WITH NumberedRows AS
	(
	SELECT 
		dbo.Message.MessageID, dbo.Message.Message, dbo.Message.Date, dbo.Message.Category, dbo.Message.Priority,
		ROW_NUMBER() OVER (ORDER BY '+@Orderby+' '+@Direction+') AS RowNumber
	FROM dbo.LogRel
	)
	SELECT MessageID, Message, Date, Category, Priority
	FROM NumberedRows
	WHERE RowNumber BETWEEN '+CONVERT(varchar(5), @From)+' AND '+CONVERT(varchar(5), @To);


	--WITH NumberedRows AS
	--(
	--SELECT 
	--	dbo.Message.MessageID, dbo.Message.Message, dbo.Message.Date, dbo.Message.Category, dbo.Message.Priority,
	--	ROW_NUMBER() OVER (ORDER BY dbo.Message.Category DESC) AS RowNumber
	--FROM dbo.LogRel
	--LEFT JOIN dbo.Message on dbo.LogRel.MessageID = dbo.Message.MessageID
	--WHERE (dbo.LogRel.ProcessID = 1)
	--)
	--SELECT MessageID, Message, Date, Category, Priority
	--FROM NumberedRows
	--WHERE RowNumber BETWEEN 0 AND 1


	--EXECUTE sp_executesql @SQLString, N'@P_ID int', @P_ID = @ProcessID 
	EXECUTE sp_executesql @SQLString

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetPageOfMessageSummary]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kyle Paulsen
-- Create date: 2012-01-31
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetPageOfMessageSummary] 
	-- Add the parameters for the stored procedure here
	(
		@processID int = -1,
		@priority int = -1,
		@startAndStop int = 0,
		@SortBy int = 1,
		@Order int = 0,
		@Num int = 10
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

	DECLARE @SQLString nvarchar(1200);
	DECLARE @Orderby nvarchar(100);
	DECLARE @GetPrior nvarchar(100);
	DECLARE @GetStartStop nvarchar(100);
	DECLARE @whereprocid nvarchar(100);
	DECLARE @Direction nvarchar(4);

	IF(@SortBy = 0) 
		SET @Orderby = 'ProcName';
	ELSE
	IF(@SortBy = 2)
		SET @Orderby = 'Message';
	ELSE
	IF(@SortBy = 5)
		SET @Orderby = 'Priority';
	ELSE
		SET @Orderby = 'Date';
	

	IF(@Order = 0) 
		SET @Direction = 'DESC';
	ELSE
		SET @Direction = 'ASC';

	IF(@priority = 0)
		SET @GetPrior = '(0)';
	ELSE
	IF(@priority = 1)
		SET @GetPrior = '(1)';
	ELSE
	IF(@priority = 2)
		SET @GetPrior = '(2)';
	ELSE
		SET @GetPrior = '(0,1,2)';

	IF(@startAndStop = 1)
		SET @GetStartStop = ' AND Category < 2 ';
	ELSE
		SET @GetStartStop = '';

	IF(@processID = -1)
		SET @whereprocid = '';
	ELSE
		SET @whereprocid = ' AND ProcID = '+CONVERT(varchar(5), @processID)+' ';

	SET @SQLString = 
	N'
	WITH SummaryTable AS (
		SELECT 
			msg.MessageID,
			msg.Message,
			msg.Priority,
			msg.Category,
			msg.Date,
			(SELECT TOP 1 pro.ProcessName FROM dbo.Message AS msg2 LEFT JOIN dbo.LogRel AS lo ON msg2.MessageID = lo.MessageID LEFT JOIN dbo.Process AS pro ON lo.ProcessID = pro.ProcessID WHERE msg2.MessageID = msg.MessageID) AS ProcName,
			(SELECT TOP 1 pro.ProcessID FROM dbo.Message AS msg2 LEFT JOIN dbo.LogRel AS lo ON msg2.MessageID = lo.MessageID LEFT JOIN dbo.Process AS pro ON lo.ProcessID = pro.ProcessID WHERE msg2.MessageID = msg.MessageID) AS ProcID,
			(SELECT TOP 1 pro.State FROM dbo.Message AS msg2 LEFT JOIN dbo.LogRel AS lo ON msg2.MessageID = lo.MessageID LEFT JOIN dbo.Process AS pro ON lo.ProcessID = pro.ProcessID WHERE msg2.MessageID = msg.MessageID) AS ProcState
		FROM
			dbo.Message AS msg
	)
	SELECT TOP '+CONVERT(varchar(5), @Num)+'
		MessageID, Message, Priority, Category, Date, ProcName, ProcID, ProcState 
	FROM SummaryTable
	WHERE
		Priority IN '+@GetPrior+@GetStartStop+@whereprocid+' 
	ORDER BY '+@Orderby+' '+@Direction
	
	/*
	SELECT TOP 10
		msg.MessageID,
		msg.Message,
		msg.Priority,
		msg.Category,
		msg.Date,
		(SELECT TOP 1 pro.ProcessName FROM dbo.Message AS msg2 LEFT JOIN dbo.LogRel AS lo ON msg2.MessageID = lo.MessageID LEFT JOIN dbo.Process AS pro ON lo.ProcessID = pro.ProcessID WHERE msg2.MessageID = msg.MessageID) AS ProcName,
		(SELECT TOP 1 pro.ProcessID FROM dbo.Message AS msg2 LEFT JOIN dbo.LogRel AS lo ON msg2.MessageID = lo.MessageID LEFT JOIN dbo.Process AS pro ON lo.ProcessID = pro.ProcessID WHERE msg2.MessageID = msg.MessageID) AS ProcID,
		(SELECT TOP 1 pro.State FROM dbo.Message AS msg2 LEFT JOIN dbo.LogRel AS lo ON msg2.MessageID = lo.MessageID LEFT JOIN dbo.Process AS pro ON lo.ProcessID = pro.ProcessID WHERE msg2.MessageID = msg.MessageID) AS ProcState
	FROM
		dbo.Message AS msg
	WHERE
		msg.Priority IN (1)
	ORDER BY msg.Message DESC
	*/
	
	EXECUTE sp_executesql @SQLString

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetPageOfProcessSummaryWithPercent]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kyle Paulsen
-- Create date: 2012-01-31
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetPageOfProcessSummaryWithPercent] 
	-- Add the parameters for the stored procedure here
	(
		@SortBy int = 1,
		@Order int = 0,
		@Num int = 10
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

	DECLARE @SQLString nvarchar(700);
	DECLARE @Orderby nvarchar(100);
	DECLARE @Direction nvarchar(4);

	IF(@SortBy = 0) 
		SET @Orderby = 'pro.ProcessName';
	ELSE
	IF(@SortBy = 2)
		SET @Orderby = 'LastMsg';
	ELSE
	IF(@SortBy = 3)
		SET @Orderby = 'MsgDate';
	ELSE
		SET @Orderby = 'pro.State'; --sortby should be 1


	IF(@Order = 0) 
		SET @Direction = 'DESC';
	ELSE
		SET @Direction = 'ASC';


	SET @SQLString = 
	N'
	SELECT TOP '+CONVERT(varchar(5), @Num)+'
		pro.ProcessID,
		pro.ProcessName,
		pro.State,
		(SELECT TOP 1 dbo.Job.PercentComplete FROM dbo.Job) AS Percentage,
		(SELECT TOP 1 msg.Message FROM dbo.Message AS msg LEFT JOIN dbo.LogRel AS lo ON msg.MessageID = lo.MessageID LEFT JOIN dbo.Process AS pro2 ON lo.ProcessID = pro2.ProcessID WHERE lo.ProcessID = pro.ProcessID ORDER BY msg.Date DESC) AS LastMsg,
		(SELECT TOP 1 msg.Date FROM dbo.Message AS msg LEFT JOIN dbo.LogRel AS lo ON msg.MessageID = lo.MessageID LEFT JOIN dbo.Process AS pro2 ON lo.ProcessID = pro2.ProcessID WHERE lo.ProcessID = pro.ProcessID ORDER BY msg.Date DESC) AS MsgDate
	FROM
		dbo.Process AS pro
	ORDER BY '+@Orderby+' '+@Direction
	
	/*
	SELECT TOP 10
		pro.ProcessID,
		pro.ProcessName,
		pro.State,
		(SELECT TOP 1 msg.Message FROM dbo.Message AS msg LEFT JOIN dbo.LogRel AS lo ON msg.MessageID = lo.MessageID LEFT JOIN dbo.Process AS pro2 ON lo.ProcessID = pro2.ProcessID WHERE lo.ProcessID = pro.ProcessID ORDER BY msg.Date DESC) AS LastMsg,
		(SELECT TOP 1 msg.Date FROM dbo.Message AS msg LEFT JOIN dbo.LogRel AS lo ON msg.MessageID = lo.MessageID LEFT JOIN dbo.Process AS pro2 ON lo.ProcessID = pro2.ProcessID WHERE lo.ProcessID = pro.ProcessID ORDER BY msg.Date DESC) AS MsgDate
	FROM
		dbo.Process AS pro
	ORDER BY MsgDate DESC
	*/
	
	EXECUTE sp_executesql @SQLString

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetPageOfProcessSummary]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kyle Paulsen
-- Create date: 2012-01-31
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetPageOfProcessSummary] 
	-- Add the parameters for the stored procedure here
	(
		@SortBy int = 1,
		@Order int = 0,
		@Num int = 10
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

	DECLARE @SQLString nvarchar(1200);
	DECLARE @Orderby nvarchar(100);
	DECLARE @Direction nvarchar(4);

	IF(@SortBy = 0) 
		SET @Orderby = 'pro.ProcessName';
	ELSE
	IF(@SortBy = 2)
		SET @Orderby = 'LastMsg';
	ELSE
	IF(@SortBy = 3)
		SET @Orderby = 'MsgDate';
	ELSE
		SET @Orderby = 'pro.State'; --sortby should be 1


	IF(@Order = 0) 
		SET @Direction = 'DESC';
	ELSE
		SET @Direction = 'ASC';


	SET @SQLString = 
	N'
	SELECT TOP '+CONVERT(varchar(5), @Num)+'
		pro.ProcessID,
		pro.ProcessName,
		pro.State,
		(SELECT TOP 1 j.PercentComplete FROM dbo.Job AS j LEFT JOIN dbo.LogRel AS lo ON j.JobID = lo.JobID LEFT JOIN dbo.Process AS pro2 ON lo.ProcessID = pro2.ProcessID LEFT JOIN dbo.Message AS msg ON lo.MessageID = msg.MessageID WHERE lo.ProcessID = pro.ProcessID ORDER BY msg.Date DESC) AS Percentage,
		(SELECT TOP 1 msg.Message FROM dbo.Message AS msg LEFT JOIN dbo.LogRel AS lo ON msg.MessageID = lo.MessageID LEFT JOIN dbo.Process AS pro2 ON lo.ProcessID = pro2.ProcessID WHERE lo.ProcessID = pro.ProcessID ORDER BY msg.Date DESC) AS LastMsg,
		(SELECT TOP 1 msg.Date FROM dbo.Message AS msg LEFT JOIN dbo.LogRel AS lo ON msg.MessageID = lo.MessageID LEFT JOIN dbo.Process AS pro2 ON lo.ProcessID = pro2.ProcessID WHERE lo.ProcessID = pro.ProcessID ORDER BY msg.Date DESC) AS MsgDate,
		(SELECT TOP 1 msg.Category FROM dbo.Message AS msg LEFT JOIN dbo.LogRel AS lo ON msg.MessageID = lo.MessageID LEFT JOIN dbo.Process AS pro2 ON lo.ProcessID = pro2.ProcessID WHERE lo.ProcessID = pro.ProcessID ORDER BY msg.Date DESC) AS MsgType
	FROM
		dbo.Process AS pro
	ORDER BY '+@Orderby+' '+@Direction
	
	/*
	SELECT TOP 10
		pro.ProcessID,
		pro.ProcessName,
		pro.State,
		(SELECT TOP 1 j.PercentComplete FROM dbo.Job AS j LEFT JOIN dbo.LogRel AS lo ON j.JobID = lo.JobID LEFT JOIN dbo.Process AS pro2 ON lo.ProcessID = pro2.ProcessID LEFT JOIN dbo.Message AS msg ON lo.MessageID = msg.MessageID WHERE lo.ProcessID = pro.ProcessID ORDER BY msg.Date DESC) AS Percentage,
		(SELECT TOP 1 msg.Message FROM dbo.Message AS msg LEFT JOIN dbo.LogRel AS lo ON msg.MessageID = lo.MessageID LEFT JOIN dbo.Process AS pro2 ON lo.ProcessID = pro2.ProcessID WHERE lo.ProcessID = pro.ProcessID ORDER BY msg.Date DESC) AS LastMsg,
		(SELECT TOP 1 msg.Date FROM dbo.Message AS msg LEFT JOIN dbo.LogRel AS lo ON msg.MessageID = lo.MessageID LEFT JOIN dbo.Process AS pro2 ON lo.ProcessID = pro2.ProcessID WHERE lo.ProcessID = pro.ProcessID ORDER BY msg.Date DESC) AS MsgDate,
		(SELECT TOP 1 msg.Category FROM dbo.Message AS msg LEFT JOIN dbo.LogRel AS lo ON msg.MessageID = lo.MessageID LEFT JOIN dbo.Process AS pro2 ON lo.ProcessID = pro2.ProcessID WHERE lo.ProcessID = pro.ProcessID ORDER BY msg.Date DESC) AS MsgType
	FROM
		dbo.Process AS pro
	ORDER BY MsgDate DESC
	*/
	
	EXECUTE sp_executesql @SQLString

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetPageMessagesWithProcessID]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kyle Palsen / Weixiong Lu
-- Create date: 2012-02-04
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetPageMessagesWithProcessID] 
	-- Add the parameters for the stored procedure here
	(
		@ProcessID int,
		@SortBy int = 3,
		@Order int = 0,
		@From int = 0,
		@To int = 10
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

	DECLARE @SQLString nvarchar(700);
	DECLARE @Orderby nvarchar(100);
	DECLARE @Direction nvarchar(4);

	IF(@SortBy = 2) 
		SET @Orderby = 'dbo.Message.Message';
	ELSE
	IF(@SortBy = 3)
		SET @Orderby = 'dbo.Message.Date';
	ELSE
	IF(@SortBy = 4)
		SET @Orderby = 'dbo.Message.Category';
	ELSE
	IF(@SortBy = 5)
		SET @Orderby = 'dbo.Message.Priority';


	IF(@Order = 0) 
		SET @Direction = 'DESC';
	ELSE
		SET @Direction = 'ASC';


	SET @SQLString = 
	N'WITH NumberedRows AS
	(
	SELECT 
		dbo.Message.MessageID, dbo.Message.Message, dbo.Message.Date, dbo.Message.Category, dbo.Message.Priority,
		ROW_NUMBER() OVER (ORDER BY '+@Orderby+' '+@Direction+') AS RowNumber
	FROM dbo.LogRel
	LEFT JOIN dbo.Message on dbo.LogRel.MessageID = dbo.Message.MessageID
	WHERE (dbo.LogRel.ProcessID = '+CONVERT(varchar(5), @PRocessID)+')
	)
	SELECT MessageID, Message, Date, Category, Priority
	FROM NumberedRows
	WHERE RowNumber BETWEEN '+CONVERT(varchar(5), @From)+' AND '+CONVERT(varchar(5), @To);


	--WITH NumberedRows AS
	--(
	--SELECT 
	--	dbo.Message.MessageID, dbo.Message.Message, dbo.Message.Date, dbo.Message.Category, dbo.Message.Priority,
	--	ROW_NUMBER() OVER (ORDER BY dbo.Message.Category DESC) AS RowNumber
	--FROM dbo.LogRel
	--LEFT JOIN dbo.Message on dbo.LogRel.MessageID = dbo.Message.MessageID
	--WHERE (dbo.LogRel.ProcessID = 1)
	--)
	--SELECT MessageID, Message, Date, Category, Priority
	--FROM NumberedRows
	--WHERE RowNumber BETWEEN 0 AND 1


	--EXECUTE sp_executesql @SQLString, N'@P_ID int', @P_ID = @ProcessID 
	EXECUTE sp_executesql @SQLString

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetPageMessagesWithCategory]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kyle Palsen / Weixiong Lu
-- Create date: 2012-02-04
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetPageMessagesWithCategory] 
	-- Add the parameters for the stored procedure here
	(
		@Category int,
		@SortBy int = 3,
		@Order int = 0,
		@From int = 0,
		@To int = 10
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

	DECLARE @SQLString nvarchar(700);
	DECLARE @Orderby nvarchar(100);
	DECLARE @Direction nvarchar(4);

	IF(@SortBy = 2) 
		SET @Orderby = 'dbo.Message.Message';
	ELSE
	IF(@SortBy = 3)
		SET @Orderby = 'dbo.Message.Date';
	ELSE
	IF(@SortBy = 4)
		SET @Orderby = 'dbo.Message.Category';
	ELSE
	IF(@SortBy = 5)
		SET @Orderby = 'dbo.Message.Priority';


	IF(@Order = 0) 
		SET @Direction = 'DESC';
	ELSE
		SET @Direction = 'ASC';


	SET @SQLString = 
	N'WITH NumberedRows AS
	(
	SELECT 
		dbo.Message.MessageID, dbo.Message.Message, dbo.Message.Date, dbo.Message.Category, dbo.Message.Priority,
		dbo.Process.ProcessID, dbo.Process.ProcessName, dbo.Process.State, 
		ROW_NUMBER() OVER (ORDER BY '+@Orderby+' '+@Direction+') AS RowNumber
	FROM dbo.LogRel
	LEFT JOIN dbo.Message on dbo.LogRel.MessageID = dbo.Message.MessageID
	LEFT JOIN dbo.Process on dbo.LogRel.ProcessID = dbo.Process.ProcessID
	WHERE dbo.Message.Category = '+CONVERT(varchar(5), @Category)+'

	)

	SELECT MessageID, Message, Date, Category, Priority, ProcessID, ProcessName, State 
	FROM NumberedRows
	WHERE RowNumber BETWEEN '+CONVERT(varchar(5), @From)+' AND '+CONVERT(varchar(5), @To);


	--WITH NumberedRows AS
	--(
	--SELECT 
	--	dbo.Message.MessageID, dbo.Message.Message, dbo.Message.Date, dbo.Message.Category, dbo.Message.Priority,
	--	dbo.Process.ProcessID, dbo.Process.ProcessName, dbo.Process.State, 
	--	ROW_NUMBER() OVER (ORDER BY dbo.Message.Priority DESC) AS RowNumber
	--FROM dbo.LogRel
	--LEFT JOIN dbo.Process on dbo.LogRel.ProcessID = dbo.Process.ProcessID
	--WHERE dbo.Message.Category = 5
	--)

	--SELECT MessageID, Message, Date, Category, Priority, ProcessID, ProcessName, State 
	--FROM NumberedRows
	--WHERE RowNumber BETWEEN 0 AND 10

	--WITH NumberedRows AS
	--(
	--SELECT 
	--	dbo.Message.MessageID, dbo.Message.Message, dbo.Message.Date, dbo.Message.Category, dbo.Message.Priority,
	--	ROW_NUMBER() OVER (ORDER BY dbo.Message.Category DESC) AS RowNumber
	--FROM dbo.LogRel
	--LEFT JOIN dbo.Message on dbo.LogRel.MessageID = dbo.Message.MessageID
	--WHERE (dbo.LogRel.ProcessID = 1)
	--)
	--SELECT MessageID, Message, Date, Category, Priority
	--FROM NumberedRows
	--WHERE RowNumber BETWEEN 0 AND 1


	--EXECUTE sp_executesql @SQLString, N'@P_ID int', @P_ID = @ProcessID 
	EXECUTE sp_executesql @SQLString

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetMessagesWithProcessID]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kyle Palsen / Weixiong Lu
-- Create date: 2012-02-04
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetMessagesWithProcessID] 
	-- Add the parameters for the stored procedure here
	(
		@ProcessID int,
		@SortBy int = 3,
		@Order int = 0,
		@From int = 0,
		@To int = 10
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

	DECLARE @SQLString nvarchar(700);
	DECLARE @Orderby nvarchar(100);
	DECLARE @Direction nvarchar(4);

	IF(@SortBy = 2) 
		SET @Orderby = 'dbo.Message.Message';
	ELSE
	IF(@SortBy = 3)
		SET @Orderby = 'dbo.Message.Date';
	ELSE
	IF(@SortBy = 4)
		SET @Orderby = 'dbo.Message.Category';
	ELSE
	IF(@SortBy = 5)
		SET @Orderby = 'dbo.Message.Priority';


	IF(@Order = 0) 
		SET @Direction = 'DESC';
	ELSE
		SET @Direction = 'ASC';


	SET @SQLString = 
	N'WITH NumberedRows AS
	(
	SELECT 
		dbo.Message.MessageID, dbo.Message.Message, dbo.Message.Date, dbo.Message.Category, dbo.Message.Priority,
		ROW_NUMBER() OVER (ORDER BY '+@Orderby+' '+@Direction+') AS RowNumber
	FROM dbo.LogRel
	LEFT JOIN dbo.Message on dbo.LogRel.MessageID = dbo.Message.MessageID
	WHERE (dbo.LogRel.ProcessID = '+CONVERT(varchar(5), @PRocessID)+')
	)
	SELECT MessageID, Message, Date, Category, Priority
	FROM NumberedRows
	WHERE RowNumber BETWEEN '+CONVERT(varchar(5), @From)+' AND '+CONVERT(varchar(5), @To);

/*
	WITH NumberedRows AS
	(
	SELECT 
		dbo.Message.MessageID, dbo.Message.Message, dbo.Message.Date, dbo.Message.Category, dbo.Message.Priority,
		ROW_NUMBER() OVER (ORDER BY dbo.Message.Category DESC) AS RowNumber
	FROM dbo.LogRel
	LEFT JOIN dbo.Message on dbo.LogRel.MessageID = dbo.Message.MessageID
	WHERE (dbo.LogRel.ProcessID = 1)
	)
	SELECT MessageID, Message, Date, Category, Priority
	FROM NumberedRows
	WHERE RowNumber BETWEEN 0 AND 1

*/
	--EXECUTE sp_executesql @SQLString, N'@P_ID int', @P_ID = @ProcessID 
	EXECUTE sp_executesql @SQLString

RETURN
GO
/****** Object:  Table [dbo].[Process]    Script Date: 02/28/2012 18:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Process](
	[ProcessID] [int] IDENTITY(1,1) NOT NULL,
	[ProcessName] [nvarchar](200) NOT NULL,
	[State] [int] NOT NULL,
 CONSTRAINT [PK_Process_1] PRIMARY KEY CLUSTERED 
(
	[ProcessID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Message]    Script Date: 02/28/2012 18:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[MessageID] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](500) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Category] [int] NOT NULL,
	[Priority] [int] NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[MessageID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LogRel]    Script Date: 02/28/2012 18:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogRel](
	[MessageID] [int] NOT NULL,
	[ProcessID] [int] NOT NULL,
	[JobID] [int] NULL,
 CONSTRAINT [PK_LogRel] PRIMARY KEY CLUSTERED 
(
	[MessageID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetMessageOrderbyPriority]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson / Weixiong Lu
-- Create date: 2012-02-10
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetMessageOrderbyPriority] 
	-- Add the parameters for the stored procedure here
	(
		@MessageID int
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    SELECT *
    FROM dbo.Message
	ORDER BY Priority

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetMessageOrderbyMsg]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson / Weixiong Lu
-- Create date: 2012-02-10
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetMessageOrderbyMsg] 
	-- Add the parameters for the stored procedure here

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    SELECT *
    FROM dbo.Message
	ORDER BY Message

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetMessageOrderbyID]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson / Weixiong Lu
-- Create date: 2012-02-10
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetMessageOrderbyID] 
	-- Add the parameters for the stored procedure here

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    SELECT *
    FROM dbo.Message
	ORDER BY MessageID

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetMessageOrderbyDate]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson / Weixiong Lu
-- Create date: 2012-02-10
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetMessageOrderbyDate] 
	-- Add the parameters for the stored procedure here

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    SELECT *
    FROM dbo.Message
	ORDER BY Date

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetMessageOrderbyCategory]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson / Weixiong Lu
-- Create date: 2012-02-10
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetMessageOrderbyCategory] 
	-- Add the parameters for the stored procedure here
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    SELECT *
    FROM dbo.Message
	ORDER BY Category

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetMessage]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson / Weixiong Lu
-- Create date: 2012-01-12
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetMessage] 
	-- Add the parameters for the stored procedure here
	(
		@MessageID int
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    SELECT *
    FROM dbo.Message
    WHERE (MessageID = @MessageID)

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetJobOrderbyPlannedCount]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson / Weixiong Lu
-- Create date: 2012-02-10
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetJobOrderbyPlannedCount] 
	-- Add the parameters for the stored procedure here

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    SELECT JobID, JobName, PlannedCount, CompletedCount, PercentComplete
    FROM dbo.Job
	ORDER BY PlannedCount

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetJobOrderbyPercentComplete]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson / Weixiong Lu
-- Create date: 2012-02-10
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetJobOrderbyPercentComplete] 
	-- Add the parameters for the stored procedure here

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    SELECT JobID, JobName, PlannedCount, CompletedCount, PercentComplete
    FROM dbo.Job
	ORDER BY PercentComplete

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetJobOrderbyName]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson / Weixiong Lu
-- Create date: 2012-02-10
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetJobOrderbyName] 
	-- Add the parameters for the stored procedure here

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    SELECT JobID, JobName, PlannedCount, CompletedCount, PercentComplete
    FROM dbo.Job
	ORDER BY JobName

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetJobOrderbyID]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson / Weixiong Lu
-- Create date: 2012-02-10
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetJobOrderbyID] 
	-- Add the parameters for the stored procedure here

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    SELECT JobID, JobName, PlannedCount, CompletedCount, PercentComplete
    FROM dbo.Job
	ORDER BY JobID

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetJobOrderbyCompletedCount]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson / Weixiong Lu
-- Create date: 2012-02-10
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetJobOrderbyCompletedCount] 
	-- Add the parameters for the stored procedure here

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    SELECT JobID, JobName, PlannedCount, CompletedCount, PercentComplete
    FROM dbo.Job
	ORDER BY CompletedCount

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetJobByName]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson / Weixiong Lu
-- Create date: 2012-01-31
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetJobByName] 
	-- Add the parameters for the stored procedure here
	(
		@JobName nvarchar(200)
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    SELECT JobID, JobName, PlannedCount, CompletedCount, PercentComplete
    FROM dbo.Job
    WHERE (JobName = @JobName)

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetJob]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson / Weixiong Lu
-- Create date: 2012-01-12
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetJob] 
	-- Add the parameters for the stored procedure here
	(
		@JobID int
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    SELECT JobID, JobName, PlannedCount, CompletedCount, PercentComplete
    FROM dbo.Job
    WHERE (JobID = @JobID)

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetPageOfProcess]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson / Weixiong Lu
-- Create date: 2012-01-31
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetPageOfProcess] 
	-- Add the parameters for the stored procedure here
	(
		@From int,
		@To int
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

WITH NumberedProcess AS
(
    SELECT
        ProcessID,
        ProcessName,
        State,
        ROW_NUMBER() OVER (ORDER BY ProcessName) AS RowNumber
    FROM
        Process
)
SELECT
    ProcessID,
    ProcessName,
    State
FROM
    NumberedProcess
WHERE
    RowNumber BETWEEN @From AND @To

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetProcessOrderbyState]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson / Weixiong Lu
-- Create date: 2012-02-10
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetProcessOrderbyState] 
	-- Add the parameters for the stored procedure here

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    SELECT ProcessID, ProcessName, State
    FROM dbo.Process
	ORDER BY State

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetProcessOrderbyName]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson / Weixiong Lu
-- Create date: 2012-02-10
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetProcessOrderbyName] 
	-- Add the parameters for the stored procedure here

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    SELECT ProcessID, ProcessName, State
    FROM dbo.Process
	ORDER BY ProcessName

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetProcessOrderbyID]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson / Weixiong Lu
-- Create date: 2012-02-10
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetProcessOrderbyID] 
	-- Add the parameters for the stored procedure here

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    SELECT ProcessID, ProcessName, State
    FROM dbo.Process
	ORDER BY ProcessID

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetProcessIDFromName]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kyle Paulsen
-- Create date: 2012-01-31
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetProcessIDFromName] 
	-- Add the parameters for the stored procedure here
	(
		@ProcessName nvarchar(200)
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

	SELECT ProcessID
    FROM dbo.Process
    WHERE (ProcessName = @ProcessName)

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetProcessByName]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson / Weixiong Lu
-- Create date: 2012-01-31
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetProcessByName] 
	-- Add the parameters for the stored procedure here
	(
		@ProcessName nvarchar(200)
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    SELECT ProcessID, ProcessName, State
    FROM dbo.Process
    WHERE (ProcessName = @ProcessName)

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetProcess]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson / Weixiong Lu
-- Create date: 2012-01-12
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetProcess] 
	-- Add the parameters for the stored procedure here
	(
		@ProcessID int
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    SELECT ProcessID, ProcessName, State
    FROM dbo.Process
    WHERE (ProcessID = @ProcessID)

RETURN
GO
/****** Object:  StoredProcedure [dbo].[InsertJob]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Author   : Weixiong Lu
Version  : 1.0.0
Date     : 01/08/2012
Copyright: Capstone Project Team Falcon 2012 All right reserved
*/


CREATE PROCEDURE [dbo].[InsertJob]
(
	@JobID int,
    @JobName nvarchar(200),
    @PlannedCount int,
	@CompletedCount int,
	@PercentComplete float
)

AS
    SET NOCOUNT OFF;
	
	INSERT INTO [dbo].[Job] (JobName,PlannedCount,CompletedCount,PercentComplete)
	VALUES (@JobName,@PlannedCount,@CompletedCount,@PercentComplete)
	SET @JobID = IDENT_CURRENT('dbo.Job');

	SELECT * FROM dbo.Job WHERE (JobID = @JobID)


RETURN
GO
/****** Object:  StoredProcedure [dbo].[InsertProcess2]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson /Weixiong Lu
-- Create date: 2012-01-04
-- Modify date: 2012-01-08
-- Description:	Create a new process
-- =============================================
CREATE PROCEDURE [dbo].[InsertProcess2] 
	-- Add the parameters for the stored procedure here
	(
		@ProcessID int OUTPUT,
		@ProcessName nvarchar(200) = NULL, 
		@State int = 0
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	
	INSERT INTO dbo.Process (ProcessName, State)
       VALUES (@ProcessName, @State)
       SET @ProcessID = SCOPE_IDENTITY();

RETURN
GO
/****** Object:  StoredProcedure [dbo].[InsertProcess]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson /Weixiong Lu
-- Create date: 2012-01-04
-- Modify date: 2012-01-08
-- Description:	Create a new process
-- =============================================
CREATE PROCEDURE [dbo].[InsertProcess] 
	-- Add the parameters for the stored procedure here
	(
		@ProcessID int,
		@ProcessName nvarchar(200) = NULL, 
		@State int = 0
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	
	INSERT INTO dbo.Process (ProcessName, State)
       VALUES (@ProcessName, @State)
       SET @ProcessID = IDENT_CURRENT('dbo.Process');

	 SELECT ProcessID, ProcessName, State FROM dbo.Process WHERE (ProcessID = @ProcessID)

RETURN
GO
/****** Object:  StoredProcedure [dbo].[InsertMessage]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Author   : Weixiong Lu
Version  : 1.0.0
Date     : 01/08/2012
Copyright: Capstone Project Team Falcon 2012 All right reserved
*/


CREATE PROCEDURE [dbo].[InsertMessage]
(
    @MessageID int,
    @Message nvarchar(500),
    @Date datetime,
	@Category int,
	@Priority int
)

AS
    SET NOCOUNT OFF;
	
	INSERT INTO [dbo].[Message] (Message,Date,Category,Priority)
	VALUES (@Message,@Date,@Category,@Priority)
	SET @MessageID = IDENT_CURRENT('dbo.Message');

	SELECT * FROM dbo.Message WHERE (MessageID = @MessageID)


RETURN
GO
/****** Object:  StoredProcedure [dbo].[SearchProcessByName]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kyle Paulsen / Weixiong Lu
-- Create date: 2012-02-7
-- Description:	Retrieve process by Process
-- =============================================
CREATE PROCEDURE [dbo].[SearchProcessByName] 
	-- Add the parameters for the stored procedure here
	(
		@ProcessNamePartial nvarchar(200)
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    SELECT ProcessID, ProcessName, State
    FROM dbo.Process
    WHERE (ProcessName LIKE '%'+@ProcessNamePartial+'%')
	ORDER BY ProcessName

RETURN
GO
/****** Object:  StoredProcedure [dbo].[SearchMessageBySting]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kyle Paulsen / Weixiong Lu
-- Create date: 2012-02-10
-- Description:	Retrieve process by Process
-- =============================================
CREATE PROCEDURE [dbo].[SearchMessageBySting] 
	-- Add the parameters for the stored procedure here
	(
		@MessagePartial nvarchar(500)
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    SELECT MessageID, Message, Category, Priority
    FROM dbo.Message
    WHERE (Message LIKE '%'+@MessagePartial+'%')
	ORDER BY Message

RETURN
GO
/****** Object:  StoredProcedure [dbo].[SearchJobByName]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kyle Paulsen / Weixiong Lu
-- Create date: 2012-02-10
-- Description:	Retrieve process by Process
-- =============================================
CREATE PROCEDURE [dbo].[SearchJobByName] 
	-- Add the parameters for the stored procedure here
	(
		@JobNamePartial nvarchar(200)
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    SELECT JobID,JobName,PlannedCount,CompletedCount,PercentComplete
    FROM dbo.Job
    WHERE (JobName LIKE '%'+@JobNamePartial+'%')
	ORDER BY JobName

RETURN
GO
/****** Object:  StoredProcedure [dbo].[ShowJobs]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Author   : Weixiong Lu
Version  : 1.0.0
Date     : 01/06/2012
Copyright: Capstone Project Team Falcon 2012 All right reserved
*/

CREATE PROCEDURE [dbo].[ShowJobs]
	/*
	(
	@parameter1 int = 5,
	@parameter2 datatype OUTPUT
	)
	*/
AS
	/* SET NOCOUNT ON */
	
	SELECT     JobID, JobName, PlannedCount, CompletedCount, PercentComplete
	FROM         dbo.Job

	RETURN
GO
/****** Object:  StoredProcedure [dbo].[UpdateProcess]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Author   : Weixiong Lu
Version  : 1.0.1
Date     : 01/08/2012
Copyright: Capstone Project Team Falcon 2012 All right reserved
*/


CREATE PROCEDURE [dbo].[UpdateProcess]
(
    @ProcessID int,
    @ProcessName nvarchar(200),
    @State int
)

AS
    SET NOCOUNT OFF;
	UPDATE [dbo].[Process] SET [ProcessName] = @ProcessName, [State] = @State 
	WHERE (([ProcessID] = @ProcessID));


	SELECT ProcessID, ProcessName, State FROM dbo.Process WHERE (ProcessID = @ProcessID)

RETURN
GO
/****** Object:  StoredProcedure [dbo].[UpdateMessage]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Author   : Weixiong Lu
Version  : 1.0.1
Date     : 01/08/2012
Copyright: Capstone Project Team Falcon 2012 All right reserved
*/


CREATE PROCEDURE [dbo].[UpdateMessage]
(
    @MessageID int,
    @Message nvarchar(500),
    @Date datetime,
	@Category int,
	@Priority int
)

AS
    SET NOCOUNT OFF;
	UPDATE [dbo].[Message] SET [Message] = @Message, [Date] = @Date, [Category] = @Category, [Priority] = @Priority   
	WHERE (([MessageID] = @MessageID));


	SELECT * FROM dbo.Message WHERE (MessageID = @MessageID)

RETURN
GO
/****** Object:  StoredProcedure [dbo].[UpdateJob]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Author   : Weixiong Lu
Version  : 1.0.1
Date     : 01/08/2012
Copyright: Capstone Project Team Falcon 2012 All right reserved
*/


CREATE PROCEDURE [dbo].[UpdateJob]
(
    @JobID int,
    @JobName nvarchar(200),
    @PlannedCount int,
	@CompletedCount int,
	@PercentComplete float
)

AS
    SET NOCOUNT OFF;
	UPDATE [dbo].[Job] SET [JobName] = @JobName, [PlannedCount] = @PlannedCount, [CompletedCount] = @CompletedCount, [PercentComplete] = @PercentComplete   
	WHERE (([JobID] = @JobID));


	SELECT * FROM dbo.Job WHERE (JobID = @JobID)

RETURN
GO
/****** Object:  StoredProcedure [dbo].[ShowProcesses]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Author   : Weixiong Lu
Version  : 1.0.0
Date     : 01/06/2012
Copyright: Capstone Project Team Falcon 2012 All right reserved
*/

CREATE PROCEDURE [dbo].[ShowProcesses]
AS
    SET NOCOUNT ON;
SELECT     ProcessID, ProcessName, State
FROM         dbo.Process

return
GO
/****** Object:  StoredProcedure [dbo].[ShowMessages]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Author   : Weixiong Lu
Version  : 1.0.0
Date     : 01/06/2012
Copyright: Capstone Project Team Falcon 2012 All right reserved
*/


CREATE PROCEDURE [dbo].[ShowMessages]
	/*
	(
	@parameter1 int = 5,
	@parameter2 datatype OUTPUT
	)
	*/
AS
	/* SET NOCOUNT ON */
	SELECT     MessageID, Message, Date, Category, Priority
	FROM         dbo.Message

	RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetTable1]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Author   : Weixiong Lu
Version  : 1.0.0
Date     : 01/06/2012
Copyright: Capstone Project Team Falcon 2012 All right reserved
*/



CREATE PROCEDURE [dbo].[GetTable1]
	/*
	(
	@parameter1 int = 5,
	@parameter2 datatype OUTPUT
	)
	*/
AS
	/* SET NOCOUNT ON */
	
	Select *
	from Process 
	RETURN
GO
/****** Object:  View [dbo].[ViewJob]    Script Date: 02/28/2012 18:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewJob]
AS
SELECT     dbo.Job.*
FROM         dbo.Job
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Job"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 200
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewJob'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewJob'
GO
/****** Object:  View [dbo].[ViewProcess]    Script Date: 02/28/2012 18:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewProcess]
AS
SELECT     dbo.Process.*
FROM         dbo.Process
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Process"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 99
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewProcess'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewProcess'
GO
/****** Object:  View [dbo].[ViewMessage]    Script Date: 02/28/2012 18:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewMessage]
AS
SELECT     MessageID, Message, Date, Category, Priority
FROM         dbo.Message
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Message"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewMessage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewMessage'
GO
/****** Object:  View [dbo].[ViewLogRel]    Script Date: 02/28/2012 18:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewLogRel]
AS
SELECT     MessageID, ProcessID, JobID
FROM         dbo.LogRel
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "LogRel"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 99
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewLogRel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewLogRel'
GO
/****** Object:  View [dbo].[ViewAll]    Script Date: 02/28/2012 18:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewAll]
AS
SELECT     dbo.Message.MessageID, dbo.Message.Message, dbo.Message.Date, dbo.Message.Category, dbo.Message.Priority, dbo.Process.ProcessID, 
                      dbo.Process.ProcessName, dbo.Process.State, dbo.Job.JobID, dbo.Job.JobName, dbo.Job.PlannedCount, dbo.Job.CompletedCount, dbo.Job.PercentComplete
FROM         dbo.Job INNER JOIN
                      dbo.LogRel ON dbo.Job.JobID = dbo.LogRel.JobID INNER JOIN
                      dbo.Message ON dbo.LogRel.MessageID = dbo.Message.MessageID INNER JOIN
                      dbo.Process ON dbo.LogRel.ProcessID = dbo.Process.ProcessID
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Job"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 200
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "LogRel"
            Begin Extent = 
               Top = 6
               Left = 238
               Bottom = 99
               Right = 389
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Message"
            Begin Extent = 
               Top = 6
               Left = 427
               Bottom = 114
               Right = 578
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Process"
            Begin Extent = 
               Top = 6
               Left = 616
               Bottom = 99
               Right = 767
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewAll'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewAll'
GO
/****** Object:  StoredProcedure [dbo].[ShowLogRel]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Author   : Weixiong Lu
Version  : 1.0.0
Date     : 01/08/2012
Copyright: Capstone Project Team Falcon 2012 All right reserved
*/


CREATE PROCEDURE [dbo].[ShowLogRel]
	/*
	(
	@parameter1 int = 5,
	@parameter2 datatype OUTPUT
	)
	*/
AS
	/* SET NOCOUNT ON */

	SELECT     MessageID, ProcessID, JobID
	FROM         dbo.LogRel

	RETURN
GO
/****** Object:  StoredProcedure [dbo].[ShowAll]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Author   : Weixiong Lu
Version  : 1.0.0
Date     : 01/08/2012
Copyright: Capstone Project Team Falcon 2012 All right reserved
*/

CREATE PROCEDURE [dbo].[ShowAll]
	/*
	(
	@parameter1 int = 5,
	@parameter2 datatype OUTPUT
	)
	*/
AS
	/* SET NOCOUNT ON */
	SELECT     dbo.Message.MessageID, dbo.Message.Message, dbo.Message.Date, dbo.Message.Category, dbo.Message.Priority, dbo.Process.ProcessID, 
                      dbo.Process.ProcessName, dbo.Process.State, dbo.Job.JobID, dbo.Job.JobName, dbo.Job.PlannedCount, dbo.Job.CompletedCount, dbo.Job.PercentComplete
	FROM         dbo.Job INNER JOIN
						  dbo.LogRel ON dbo.Job.JobID = dbo.LogRel.JobID INNER JOIN
						  dbo.Message ON dbo.LogRel.MessageID = dbo.Message.MessageID INNER JOIN
						  dbo.Process ON dbo.LogRel.ProcessID = dbo.Process.ProcessID

	RETURN
GO
/****** Object:  StoredProcedure [dbo].[InsertLogRel]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Author   : Weixiong Lu
Version  : 1.0.0
Date     : 01/08/2012
Copyright: Capstone Project Team Falcon 2012 All right reserved
*/


CREATE PROCEDURE [dbo].[InsertLogRel]
(
    @MessageID int,
	@ProcessID int,
	@JobID int
)

AS
    SET NOCOUNT OFF;
	
	INSERT INTO [dbo].[LogRel] (MessageID,ProcessID,JobID)
	VALUES (@MessageID,@ProcessID,@JobID)


	SELECT * FROM dbo.LogRel WHERE (MessageID = @MessageID)


RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetTopMessageFromProcessId]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson / Weixiong Lu
-- Create date: 2012-01-31
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetTopMessageFromProcessId] 
	-- Add the parameters for the stored procedure here
	(
		@ProcessId int
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    SELECT 
        TOP 1 
        Message.Category,
        Message.Date,
        Message.Message,
        Message.MessageID,
        Message.Priority
    FROM
        LogRel logRel INNER JOIN Message message
        on logRel.MessageID = message.MessageID
    WHERE
		LogRel.ProcessID = @ProcessId 
RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetProcessSummaryByName]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kyle Paulsen
-- Create date: 2012-01-31
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetProcessSummaryByName] 
	-- Add the parameters for the stored procedure here
	(
		@Name nvarchar(200)
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;
	
	SELECT
		pro.ProcessID,
		pro.ProcessName,
		pro.State,
		(SELECT TOP 1 j.PercentComplete FROM dbo.Job AS j LEFT JOIN dbo.LogRel AS lo ON j.JobID = lo.JobID LEFT JOIN dbo.Process AS pro2 ON lo.ProcessID = pro2.ProcessID LEFT JOIN dbo.Message AS msg ON lo.MessageID = msg.MessageID WHERE lo.ProcessID = pro.ProcessID ORDER BY msg.Date DESC) AS Percentage,
		(SELECT TOP 1 msg.Message FROM dbo.Message AS msg LEFT JOIN dbo.LogRel AS lo ON msg.MessageID = lo.MessageID LEFT JOIN dbo.Process AS pro2 ON lo.ProcessID = pro2.ProcessID WHERE lo.ProcessID = pro.ProcessID ORDER BY msg.Date DESC) AS LastMsg,
		(SELECT TOP 1 msg.Date FROM dbo.Message AS msg LEFT JOIN dbo.LogRel AS lo ON msg.MessageID = lo.MessageID LEFT JOIN dbo.Process AS pro2 ON lo.ProcessID = pro2.ProcessID WHERE lo.ProcessID = pro.ProcessID ORDER BY msg.Date DESC) AS MsgDate,
		(SELECT TOP 1 msg.Category FROM dbo.Message AS msg LEFT JOIN dbo.LogRel AS lo ON msg.MessageID = lo.MessageID LEFT JOIN dbo.Process AS pro2 ON lo.ProcessID = pro2.ProcessID WHERE lo.ProcessID = pro.ProcessID ORDER BY msg.Date DESC) AS MsgType
	FROM
		dbo.Process AS pro
	WHERE
		pro.ProcessName = @Name

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetPageOfProcessSummaryWithJobPercenComplete]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kyle Paulsen
-- Create date: 2012-01-31
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetPageOfProcessSummaryWithJobPercenComplete] 
	-- Add the parameters for the stored procedure here

	
	(
	    @ProcessID int
	)
	
AS
	/* SET NOCOUNT ON */
	SELECT     dbo.Process.ProcessID, dbo.Process.ProcessName, dbo.Process.State, dbo.Job.PercentComplete
	FROM         dbo.Job INNER JOIN
						  dbo.LogRel ON dbo.Job.JobID = dbo.LogRel.JobID INNER JOIN
						  dbo.Message ON dbo.LogRel.MessageID = dbo.Message.MessageID INNER JOIN
						  dbo.Process ON dbo.LogRel.ProcessID = dbo.Process.ProcessID

	WHERE dbo.Process.ProcessID = @ProcessID

	RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetPageOfMessagesByProcessId]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kyle Paulsen
-- Create date: 2012-01-31
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetPageOfMessagesByProcessId] 
	-- Add the parameters for the stored procedure here
	(
		@From int,
		@To int,
		@processID int
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

WITH NumberedMessages AS
(
    SELECT
        dbo.Message.MessageID,
        dbo.Message.Message,
        dbo.Message.Date,
		dbo.Message.Category,
		dbo.Message.Priority,
        ROW_NUMBER() OVER (ORDER BY dbo.Message.Date DESC) AS RowNumber
    FROM
        dbo.Message 
		inner join dbo.LogRel on dbo.LogRel.MessageId = dbo.Message.MessageId
	WHERE dbo.LogRel.ProcessID = @processID
		
)
SELECT
    MessageID,
    Message,
    Date,
	Category,
	Priority
FROM
    NumberedMessages
WHERE
    RowNumber BETWEEN @From AND @To

RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetPageOfJobs]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kyle Paulsen
-- Create date: 2012-01-31
-- Description:	Retrieve process by ProcessID
-- =============================================
CREATE PROCEDURE [dbo].[GetPageOfJobs] 
	-- Add the parameters for the stored procedure here
	(
		@From int,
		@To int,
		@processID int
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

WITH NumberedJobs AS
(
    SELECT DISTINCT
        dbo.Job.JobID,
        dbo.Job.JobName,
        dbo.Job.PlannedCount,
		dbo.Job.CompletedCount,
		dbo.Job.PercentComplete,
        ROW_NUMBER() OVER (ORDER BY PercentComplete ASC) AS RowNumber
    FROM
        dbo.Job 
		join dbo.LogRel on dbo.Job.JobId = dbo.LogRel.JobId
	WHERE dbo.LogRel.ProcessID = @processID
		
)
SELECT DISTINCT
    JobID,
    JobName,
    PlannedCount,
	CompletedCount,
	PercentComplete
FROM
    NumberedJobs
WHERE
    RowNumber BETWEEN @From AND @To

RETURN
GO
/****** Object:  StoredProcedure [dbo].[DeleteProcess]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Author   : Weixiong Lu
Version  : 1.0.0
Date     : 01/06/2012
Copyright: Capstone Project Team Falcon 2012 All right reserved
*/


CREATE PROCEDURE [dbo].[DeleteProcess]
	
	(
		@ProcessID int
	)
	
AS
	/* SET NOCOUNT ON */
	DELETE FROM Process
	WHERE ProcessID=@ProcessID
	
	UPDATE [dbo].[LogRel] SET [ProcessID] = NULL
	WHERE (([ProcessID] = @ProcessID));

	RETURN
GO
/****** Object:  StoredProcedure [dbo].[DeleteOldMessages]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Benson
-- Create date: 2012-02-11
-- Description:	Remove old messages. Keep @NumberToKeep most recent messages.
-- =============================================
CREATE PROCEDURE [dbo].[DeleteOldMessages] 
	-- Add the parameters for the stored procedure here
	(
		@NumberToKeep int
	)

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    DELETE FROM LogRel
    WHERE MessageID NOT IN (
		SELECT TOP (@NumberToKeep) MessageID
		FROM LogRel
		ORDER BY MessageID DESC
	)

    DELETE FROM Message
    WHERE Message.MessageID NOT IN (
		SELECT logrel.MessageID
		FROM LogRel logrel
	)

    DELETE FROM Job
    WHERE Job.JobID NOT IN (
		SELECT DISTINCT logrel.JobID
		FROM LogRel logrel
		WHERE logrel.JobID IS NOT NULL
	)

    DELETE FROM Process
    WHERE Process.ProcessID NOT IN (
		SELECT DISTINCT logrel.ProcessID
		FROM LogRel logrel
	)

RETURN
GO
/****** Object:  StoredProcedure [dbo].[DeleteMessage]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Author   : Weixiong Lu
Version  : 1.0.0
Date     : 01/08/2012
Copyright: Capstone Project Team Falcon 2012 All right reserved
*/


CREATE PROCEDURE [dbo].[DeleteMessage]
	
	(
		@MessageID int
	)
	
AS
	/* SET NOCOUNT ON */
	DELETE FROM dbo.Message
	WHERE MessageID=@MessageID

	UPDATE [dbo].[LogRel] SET [MessageID] = NULL
	WHERE (([MessageID] = @MessageID));

	RETURN
GO
/****** Object:  StoredProcedure [dbo].[DeleteJob]    Script Date: 02/28/2012 18:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Author   : Weixiong Lu
Version  : 1.0.0
Date     : 01/08/2012
Copyright: Capstone Project Team Falcon 2012 All right reserved
*/


CREATE PROCEDURE [dbo].[DeleteJob]
	
	(
		@JobID int
	)
	
AS
	/* SET NOCOUNT ON */
	DELETE FROM Job
	WHERE JobID=@JobID

	UPDATE [dbo].[LogRel] SET [JobID] = NULL
	WHERE (([JobID] = @JobID));

	RETURN
GO
/****** Object:  ForeignKey [FK_LogRel_Job]    Script Date: 02/28/2012 18:50:27 ******/
ALTER TABLE [dbo].[LogRel]  WITH CHECK ADD  CONSTRAINT [FK_LogRel_Job] FOREIGN KEY([JobID])
REFERENCES [dbo].[Job] ([JobID])
GO
ALTER TABLE [dbo].[LogRel] CHECK CONSTRAINT [FK_LogRel_Job]
GO
/****** Object:  ForeignKey [FK_LogRel_Message]    Script Date: 02/28/2012 18:50:27 ******/
ALTER TABLE [dbo].[LogRel]  WITH CHECK ADD  CONSTRAINT [FK_LogRel_Message] FOREIGN KEY([MessageID])
REFERENCES [dbo].[Message] ([MessageID])
GO
ALTER TABLE [dbo].[LogRel] CHECK CONSTRAINT [FK_LogRel_Message]
GO
/****** Object:  ForeignKey [FK_LogRel_Process]    Script Date: 02/28/2012 18:50:27 ******/
ALTER TABLE [dbo].[LogRel]  WITH CHECK ADD  CONSTRAINT [FK_LogRel_Process] FOREIGN KEY([ProcessID])
REFERENCES [dbo].[Process] ([ProcessID])
GO
ALTER TABLE [dbo].[LogRel] CHECK CONSTRAINT [FK_LogRel_Process]
GO
