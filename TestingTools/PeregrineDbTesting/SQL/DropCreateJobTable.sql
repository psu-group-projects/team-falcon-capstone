USE [PeregrineDB]
GO

/****** Object:  Table [dbo].[Job]    Script Date: 12/12/2011 17:52:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Job]') AND type in (N'U'))
DROP TABLE [dbo].[Job]
GO

USE [PeregrineDB]
GO

/****** Object:  Table [dbo].[Job]    Script Date: 12/12/2011 17:52:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Job](
	[JobID] [int] NOT NULL,
	[JobName] [nchar](10) NOT NULL,
	[PlannedCount] [int] NULL,
	[CompletedCount] [int] NULL,
	[PercentComplete] [float] NOT NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[JobID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

