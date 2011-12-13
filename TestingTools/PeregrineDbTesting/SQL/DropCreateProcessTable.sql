USE [PeregrineDB]
GO

/****** Object:  Table [dbo].[Process]    Script Date: 12/12/2011 17:52:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Process]') AND type in (N'U'))
DROP TABLE [dbo].[Process]
GO

USE [PeregrineDB]
GO

/****** Object:  Table [dbo].[Process]    Script Date: 12/12/2011 17:52:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Process](
	[ProcessID] [int] NOT NULL,
	[ProcessName] [nchar](15) NOT NULL,
	[Sate] [int] NOT NULL,
 CONSTRAINT [PK_Process_1] PRIMARY KEY CLUSTERED 
(
	[ProcessID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

