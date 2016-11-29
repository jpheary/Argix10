SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Argix_Contacts](
	[FirstName] [nvarchar](200) NULL,
	[LastName] [nvarchar](200) NULL,
	[Company] [nvarchar](300) NULL,
	[Title] [nvarchar](150) NULL,
	[Email] [nvarchar](500) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[Deconsolidation] [nvarchar](5) NULL,
	[DCByPass] [nvarchar](5) NULL,
	[Warehousing] [nvarchar](5) NULL,
	[Fulfillment] [nvarchar](5) NULL,
	[WMSIntegration] [nvarchar](5) NULL,
	[CustomShipping] [nvarchar](5) NULL,
	[DateContacted] [datetime] NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Argix_Contacts] ADD  CONSTRAINT [DF_Argix_Contacts_DateDownload]  DEFAULT (getdate()) FOR [DateContacted]
GO


