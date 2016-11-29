USE [Tsort]
GO

/****** Object:  Table [dbo].[dspTrailerLog]    Script Date: 03/03/2014 11:25:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[dspTrailerLog](
	[ID] [int] IDENTITY(500000,1) NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreateUserID] [varchar](50) NOT NULL,
	[ScheduleDate] [datetime] NOT NULL,
	[TrailerNumber] [varchar](15) NOT NULL,
	[InboundDate] [datetime] NULL,
	[InboundCarrier] [varchar](50) NULL,
	[InboundSeal] [varchar](50) NULL,
	[InboundDriverName] [varchar](50) NULL,
	[TDSNumber] [varchar](15) NULL,
	[InitialYardLocation] [varchar](50) NULL,
	[OutboundDate] [datetime] NULL,
	[OutboundCarrier] [varchar](50) NULL,
	[OutboundSeal] [varchar](50) NULL,
	[OutboundDriverName] [varchar](50) NULL,
	[BOLNumber] [varchar](15) NULL,
	[Comments] [varchar](50) NULL,
	[IsTemplate] [bit] NULL,
	[CancelledUserID] [char](50) NULL,
	[Cancelled] [datetime] NULL,
	[LastUpdated] [datetime] NOT NULL,
	[UserID] [varchar](50) NOT NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_dspTrailerLog_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[dspTrailerLog] ADD  CONSTRAINT [DF_dspTrailerLog_Created]  DEFAULT (getdate()) FOR [Created]
GO

ALTER TABLE [dbo].[dspTrailerLog] ADD  CONSTRAINT [DF_dspTrailerLog_CreateUserID]  DEFAULT (suser_sname()) FOR [CreateUserID]
GO

ALTER TABLE [dbo].[dspTrailerLog] ADD  CONSTRAINT [DF_dspTrailerLog_ScheduleDate]  DEFAULT (getdate()) FOR [ScheduleDate]
GO

ALTER TABLE [dbo].[dspTrailerLog] ADD  CONSTRAINT [DF_dspTrailerLog_Comments]  DEFAULT ('') FOR [Comments]
GO

ALTER TABLE [dbo].[dspTrailerLog] ADD  CONSTRAINT [DF_dspTrailerLog_IsTemplate]  DEFAULT ((0)) FOR [IsTemplate]
GO

ALTER TABLE [dbo].[dspTrailerLog] ADD  CONSTRAINT [DF_dspTrailerLog_LastUpdated]  DEFAULT (getdate()) FOR [LastUpdated]
GO

ALTER TABLE [dbo].[dspTrailerLog] ADD  CONSTRAINT [DF_dspTrailerLog_UserID]  DEFAULT (suser_sname()) FOR [UserID]
GO


