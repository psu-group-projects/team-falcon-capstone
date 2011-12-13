USE [PeregrineDB]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LogRel_Job]') AND parent_object_id = OBJECT_ID(N'[dbo].[LogRel]'))
ALTER TABLE [dbo].[LogRel] DROP CONSTRAINT [FK_LogRel_Job]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LogRel_Message]') AND parent_object_id = OBJECT_ID(N'[dbo].[LogRel]'))
ALTER TABLE [dbo].[LogRel] DROP CONSTRAINT [FK_LogRel_Message]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LogRel_Process]') AND parent_object_id = OBJECT_ID(N'[dbo].[LogRel]'))
ALTER TABLE [dbo].[LogRel] DROP CONSTRAINT [FK_LogRel_Process]
GO

USE [PeregrineDB]
GO

/****** Object:  Table [dbo].[LogRel]    Script Date: 12/12/2011 17:53:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LogRel]') AND type in (N'U'))
DROP TABLE [dbo].[LogRel]
GO

USE [PeregrineDB]
GO

/****** Object:  Table [dbo].[LogRel]    Script Date: 12/12/2011 17:53:22 ******/
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

ALTER TABLE [dbo].[LogRel]  WITH CHECK ADD  CONSTRAINT [FK_LogRel_Job] FOREIGN KEY([JobID])
REFERENCES [dbo].[Job] ([JobID])
GO

ALTER TABLE [dbo].[LogRel] CHECK CONSTRAINT [FK_LogRel_Job]
GO

ALTER TABLE [dbo].[LogRel]  WITH CHECK ADD  CONSTRAINT [FK_LogRel_Message] FOREIGN KEY([MessageID])
REFERENCES [dbo].[Messages] ([MessageID])
GO

ALTER TABLE [dbo].[LogRel] CHECK CONSTRAINT [FK_LogRel_Message]
GO

ALTER TABLE [dbo].[LogRel]  WITH CHECK ADD  CONSTRAINT [FK_LogRel_Process] FOREIGN KEY([ProcessID])
REFERENCES [dbo].[Process] ([ProcessID])
GO

ALTER TABLE [dbo].[LogRel] CHECK CONSTRAINT [FK_LogRel_Process]
GO

