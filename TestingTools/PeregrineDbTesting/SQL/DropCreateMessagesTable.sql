USE [PeregrineDB]
GO

/****** Object:  Table [dbo].[Messages]    Script Date: 12/12/2011 17:52:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Messages]') AND type in (N'U'))
DROP TABLE [dbo].[Messages]
GO

USE [PeregrineDB]
GO

/****** Object:  Table [dbo].[Messages]    Script Date: 12/12/2011 17:52:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Messages](
	[MessageID] [int] NOT NULL,
	[Message] [nvarchar](50) NOT NULL,
	[Date] [date] NOT NULL,
	[Category] [int] NOT NULL,
	[Prority] [int] NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[MessageID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

