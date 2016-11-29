USE [Tsort]
GO

/****** Object:  Table [dbo].[dspPickupLog]    Script Date: 03/03/2014 11:25:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[dspPickupLog](
	[RequestID] [int] IDENTITY(400000,1) NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreateUserID] [varchar](50) NOT NULL,
	[ScheduleDate] [datetime] NOT NULL,
	[CallerName] [varchar](50) NULL,
	[ClientNumber] [varchar](3) NULL,
	[Client] [varchar](50) NOT NULL,
	[ShipperNumber] [varchar](14) NULL,
	[Shipper] [varchar](50) NOT NULL,
	[ShipperAddress] [varchar](50) NOT NULL,
	[ShipperPhone] [varchar](50) NULL,
	[WindowOpen] [smallint] NULL,
	[WindowClose] [smallint] NULL,
	[TerminalNumber] [varchar](50) NULL,
	[Terminal] [varchar](50) NULL,
	[DriverName] [varchar](50) NULL,
	[ActualPickup] [datetime] NULL,
	[OrderType] [varchar](10) NULL,
	[Amount] [int] NULL,
	[AmountType] [varchar](50) NULL,
	[FreightType] [varchar](50) NULL,
	[Weight] [int] NULL,
	[Comments] [varchar](50) NULL,
	[IsTemplate] [bit] NULL,
	[Cancelled] [datetime] NULL,
	[CancelledUserID] [char](50) NULL,
	[LastUpdated] [datetime] NOT NULL,
	[UserID] [varchar](50) NOT NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_dspPickupLog_RequestID] PRIMARY KEY CLUSTERED 
(
	[RequestID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[dspPickupLog] ADD  CONSTRAINT [DF_dspPickupLog_Created]  DEFAULT (getdate()) FOR [Created]
GO

ALTER TABLE [dbo].[dspPickupLog] ADD  CONSTRAINT [DF_dspPickupLog_CreateUserID]  DEFAULT (suser_sname()) FOR [CreateUserID]
GO

ALTER TABLE [dbo].[dspPickupLog] ADD  CONSTRAINT [DF_dspPickupLog_ScheduleDate]  DEFAULT (getdate()) FOR [ScheduleDate]
GO

ALTER TABLE [dbo].[dspPickupLog] ADD  CONSTRAINT [DF_dspPickupLog_Amount]  DEFAULT ((0)) FOR [Amount]
GO

ALTER TABLE [dbo].[dspPickupLog] ADD  CONSTRAINT [DF_dspPickupLog_AmountType]  DEFAULT ('') FOR [AmountType]
GO

ALTER TABLE [dbo].[dspPickupLog] ADD  CONSTRAINT [DF_dspPickupLog_FreightType]  DEFAULT ('') FOR [FreightType]
GO

ALTER TABLE [dbo].[dspPickupLog] ADD  CONSTRAINT [DF_dspPickupLog_Weight]  DEFAULT ((0)) FOR [Weight]
GO

ALTER TABLE [dbo].[dspPickupLog] ADD  CONSTRAINT [DF_dspPickupLog_Comments]  DEFAULT ('') FOR [Comments]
GO

ALTER TABLE [dbo].[dspPickupLog] ADD  CONSTRAINT [DF_dspPickupLog_IsTemplate]  DEFAULT ((0)) FOR [IsTemplate]
GO

ALTER TABLE [dbo].[dspPickupLog] ADD  CONSTRAINT [DF_dspPickupLog_LastUpdated]  DEFAULT (getdate()) FOR [LastUpdated]
GO

ALTER TABLE [dbo].[dspPickupLog] ADD  CONSTRAINT [DF_dspPickupLog_UserID]  DEFAULT (suser_sname()) FOR [UserID]
GO


