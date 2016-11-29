USE [Tsort]
GO

/****** Object:  Table [dbo].[dspClientInboundSheet]    Script Date: 03/03/2014 11:24:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[dspClientInboundSheet](
	[ID] [int] IDENTITY(100000,1) NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreateUserID] [varchar](50) NOT NULL,
	[ScheduleDate] [datetime] NOT NULL,
	[VendorName] [varchar](50) NOT NULL,
	[ConsigneeName] [varchar](50) NOT NULL,
	[CarrierName] [varchar](50) NULL,
	[DriverName] [varchar](50) NULL,
	[TrailerNumber] [varchar](15) NULL,
	[ScheduledArrival] [datetime] NOT NULL,
	[ActualArrival] [datetime] NULL,
	[Amount] [int] NULL,
	[AmountType] [varchar](25) NULL,
	[FreightType] [varchar](10) NULL,
	[IsLiveUnload] [bit] NULL,
	[SortDate] [datetime] NULL,
	[TDSNumber] [varchar](15) NULL,
	[TDSCreateUserID] [varchar](50) NULL,
	[Comments] [varchar](50) NULL,
	[IsTemplate] [bit] NULL,
	[CancelledUserID] [char](50) NULL,
	[Cancelled] [datetime] NULL,
	[LastUpdated] [datetime] NOT NULL,
	[UserID] [varchar](50) NOT NULL,
	[RowVersion] [timestamp] NULL,
	[ClientNumber] [varchar](3) NULL,
	[Client] [varchar](50) NULL,
 CONSTRAINT [PK_dspClientInboundSheet_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[dspClientInboundSheet] ADD  CONSTRAINT [DF_dspClientInboundSheet_Created]  DEFAULT (getdate()) FOR [Created]
GO

ALTER TABLE [dbo].[dspClientInboundSheet] ADD  CONSTRAINT [DF_dspClientInboundSheet_CreateUserID]  DEFAULT (suser_sname()) FOR [CreateUserID]
GO

ALTER TABLE [dbo].[dspClientInboundSheet] ADD  CONSTRAINT [DF_dspClientInboundSheet_ScheduleDate]  DEFAULT (getdate()) FOR [ScheduleDate]
GO

ALTER TABLE [dbo].[dspClientInboundSheet] ADD  CONSTRAINT [DF_dspClientInboundSheet_VendorName]  DEFAULT ('') FOR [VendorName]
GO

ALTER TABLE [dbo].[dspClientInboundSheet] ADD  CONSTRAINT [DF_dspClientInboundSheet_ConsigneeName]  DEFAULT ('') FOR [ConsigneeName]
GO

ALTER TABLE [dbo].[dspClientInboundSheet] ADD  CONSTRAINT [DF_dspClientInboundSheet_ScheduledArrival]  DEFAULT (getdate()) FOR [ScheduledArrival]
GO

ALTER TABLE [dbo].[dspClientInboundSheet] ADD  CONSTRAINT [DF_dspClientInboundSheet_Amount]  DEFAULT ((0)) FOR [Amount]
GO

ALTER TABLE [dbo].[dspClientInboundSheet] ADD  CONSTRAINT [DF_dspClientInboundSheet_AmountType]  DEFAULT ('') FOR [AmountType]
GO

ALTER TABLE [dbo].[dspClientInboundSheet] ADD  CONSTRAINT [DF_dspClientInboundSheet_FreightType]  DEFAULT ('') FOR [FreightType]
GO

ALTER TABLE [dbo].[dspClientInboundSheet] ADD  CONSTRAINT [DF_dspClientInboundSheet_IsLiveUnload]  DEFAULT ((0)) FOR [IsLiveUnload]
GO

ALTER TABLE [dbo].[dspClientInboundSheet] ADD  CONSTRAINT [DF_dspClientInboundSheet_Comments]  DEFAULT ('') FOR [Comments]
GO

ALTER TABLE [dbo].[dspClientInboundSheet] ADD  CONSTRAINT [DF_dspClientInboundSheet_IsTemplate]  DEFAULT ((0)) FOR [IsTemplate]
GO

ALTER TABLE [dbo].[dspClientInboundSheet] ADD  CONSTRAINT [DF_dspClientInboundSheet_LastUpdated]  DEFAULT (getdate()) FOR [LastUpdated]
GO

ALTER TABLE [dbo].[dspClientInboundSheet] ADD  CONSTRAINT [DF_dspClientInboundSheet_UserID]  DEFAULT (suser_sname()) FOR [UserID]
GO


