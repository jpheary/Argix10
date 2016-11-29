USE [Tsort]
GO

/****** Object:  Table [dbo].[dspInboundSchedule]    Script Date: 03/03/2014 11:25:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[dspInboundSchedule](
	[ID] [int] IDENTITY(200000,1) NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreateUserID] [varchar](50) NOT NULL,
	[ScheduleDate] [datetime] NOT NULL,
	[Origin] [varchar](50) NOT NULL,
	[OriginLocation] [varchar](50) NULL,
	[Destination] [varchar](50) NOT NULL,
	[DestinationLocation] [varchar](50) NULL,
	[CarrierName] [varchar](50) NULL,
	[DriverName] [varchar](50) NULL,
	[TrailerNumber] [varchar](15) NULL,
	[DropEmptyTrailerNumber] [varchar](15) NULL,
	[ScheduledDeparture] [datetime] NOT NULL,
	[ActualDeparture] [datetime] NULL,
	[ScheduledArrival] [datetime] NOT NULL,
	[ActualArrival] [datetime] NULL,
	[Confirmed] [bit] NULL,
	[Amount] [int] NULL,
	[AmountType] [varchar](25) NULL,
	[FreightType] [varchar](10) NULL,
	[Comments] [varchar](50) NULL,
	[IsTemplate] [bit] NULL,
	[CancelledUserID] [char](50) NULL,
	[Cancelled] [datetime] NULL,
	[LastUpdated] [datetime] NOT NULL,
	[UserID] [varchar](50) NOT NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_dspInboundSchedule_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[dspInboundSchedule] ADD  CONSTRAINT [DF_dspInboundSchedule_Created]  DEFAULT (getdate()) FOR [Created]
GO

ALTER TABLE [dbo].[dspInboundSchedule] ADD  CONSTRAINT [DF_dspInboundSchedule_CreateUserID]  DEFAULT (suser_sname()) FOR [CreateUserID]
GO

ALTER TABLE [dbo].[dspInboundSchedule] ADD  CONSTRAINT [DF_dspInboundSchedule_ScheduleDate]  DEFAULT (getdate()) FOR [ScheduleDate]
GO

ALTER TABLE [dbo].[dspInboundSchedule] ADD  CONSTRAINT [DF_dspInboundSchedule_Origin]  DEFAULT ('') FOR [Origin]
GO

ALTER TABLE [dbo].[dspInboundSchedule] ADD  CONSTRAINT [DF_dspInboundSchedule_OriginLocation]  DEFAULT ('') FOR [OriginLocation]
GO

ALTER TABLE [dbo].[dspInboundSchedule] ADD  CONSTRAINT [DF_dspInboundSchedule_Destination]  DEFAULT ('') FOR [Destination]
GO

ALTER TABLE [dbo].[dspInboundSchedule] ADD  CONSTRAINT [DF_dspInboundSchedule_DestinationLocation]  DEFAULT ('') FOR [DestinationLocation]
GO

ALTER TABLE [dbo].[dspInboundSchedule] ADD  CONSTRAINT [DF_dspInboundSchedule_ScheduledDeparture]  DEFAULT (getdate()) FOR [ScheduledDeparture]
GO

ALTER TABLE [dbo].[dspInboundSchedule] ADD  CONSTRAINT [DF_dspInboundSchedule_ScheduledArrival]  DEFAULT (getdate()) FOR [ScheduledArrival]
GO

ALTER TABLE [dbo].[dspInboundSchedule] ADD  CONSTRAINT [DF_dspInboundSchedule_Confirmed]  DEFAULT ((0)) FOR [Confirmed]
GO

ALTER TABLE [dbo].[dspInboundSchedule] ADD  CONSTRAINT [DF_dspInboundSchedule_Amount]  DEFAULT ((0)) FOR [Amount]
GO

ALTER TABLE [dbo].[dspInboundSchedule] ADD  CONSTRAINT [DF_dspInboundSchedule_AmountType]  DEFAULT ('') FOR [AmountType]
GO

ALTER TABLE [dbo].[dspInboundSchedule] ADD  CONSTRAINT [DF_dspInboundSchedule_FreightType]  DEFAULT ('') FOR [FreightType]
GO

ALTER TABLE [dbo].[dspInboundSchedule] ADD  CONSTRAINT [DF_dspInboundSchedule_Comments]  DEFAULT ('') FOR [Comments]
GO

ALTER TABLE [dbo].[dspInboundSchedule] ADD  CONSTRAINT [DF_dspInboundSchedule_IsTemplate]  DEFAULT ((0)) FOR [IsTemplate]
GO

ALTER TABLE [dbo].[dspInboundSchedule] ADD  CONSTRAINT [DF_dspInboundSchedule_LastUpdated]  DEFAULT (getdate()) FOR [LastUpdated]
GO

ALTER TABLE [dbo].[dspInboundSchedule] ADD  CONSTRAINT [DF_dspInboundSchedule_UserID]  DEFAULT (suser_sname()) FOR [UserID]
GO


