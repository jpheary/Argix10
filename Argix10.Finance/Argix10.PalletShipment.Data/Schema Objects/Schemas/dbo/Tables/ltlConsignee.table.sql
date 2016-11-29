USE [Tsort]
GO

/****** Object:  Table [dbo].[ltlConsignee]    Script Date: 03/03/2014 10:49:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ltlConsignee](
	[ID] [int] IDENTITY(100000,1) NOT NULL,
	[ClientID] [int] NOT NULL,
	[Name] [varchar](40) NOT NULL,
	[AddressLine1] [varchar](40) NOT NULL,
	[AddressLine2] [varchar](40) NULL,
	[City] [varchar](40) NOT NULL,
	[State] [char](2) NOT NULL,
	[Zip] [char](5) NOT NULL,
	[Zip4] [char](4) NULL,
	[ContactName] [varchar](40) NOT NULL,
	[ContactPhone] [varchar](24) NOT NULL,
	[ContactEmail] [varchar](50) NOT NULL,
	[WindowStartTime] [datetime] NULL,
	[WindowEndTime] [datetime] NULL,
	[Status] [char](1) NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[UserID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ltlConsignee] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ltlConsignee]  WITH NOCHECK ADD  CONSTRAINT [FK_ltlConsignee_ltlClient] FOREIGN KEY([ClientID])
REFERENCES [dbo].[ltlClient] ([ID])
GO

ALTER TABLE [dbo].[ltlConsignee] CHECK CONSTRAINT [FK_ltlConsignee_ltlClient]
GO

ALTER TABLE [dbo].[ltlConsignee] ADD  CONSTRAINT [DF_ltlConsignee_ContactName]  DEFAULT ('') FOR [ContactName]
GO

ALTER TABLE [dbo].[ltlConsignee] ADD  CONSTRAINT [DF_ltlConsignee_ContactPhone]  DEFAULT ('') FOR [ContactPhone]
GO

ALTER TABLE [dbo].[ltlConsignee] ADD  CONSTRAINT [DF_ltlConsignee_ContactEmail]  DEFAULT ('') FOR [ContactEmail]
GO

ALTER TABLE [dbo].[ltlConsignee] ADD  CONSTRAINT [DF_ltlConsignee_Status]  DEFAULT ('A') FOR [Status]
GO

ALTER TABLE [dbo].[ltlConsignee] ADD  CONSTRAINT [DF_ltlConsignee_LastUpdated]  DEFAULT (getdate()) FOR [LastUpdated]
GO

ALTER TABLE [dbo].[ltlConsignee] ADD  CONSTRAINT [DF_ltlConsignee_UserID]  DEFAULT (suser_sname()) FOR [UserID]
GO


