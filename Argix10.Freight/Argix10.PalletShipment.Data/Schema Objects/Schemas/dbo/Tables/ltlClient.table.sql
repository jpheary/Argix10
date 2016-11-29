CREATE TABLE [dbo].[ltlClient](
	[ID] [int] IDENTITY(100000,1) NOT NULL,
	[Name] [varchar](40) NOT NULL,
	[AddressLine1] [varchar](40) NULL,
	[AddressLine2] [varchar](40) NULL,
	[City] [varchar](40) NULL,
	[State] [char](2) NULL,
	[Zip] [char](5) NULL,
	[Zip4] [char](4) NULL,
	[ContactName] [varchar](40) NOT NULL,
	[ContactPhone] [varchar](24) NULL,
	[ContactEmail] [varchar](50) NOT NULL,
	[CorporateName] [varchar](40) NULL,
	[CorporateAddressLine1] [varchar](40) NULL,
	[CorporateAddressLine2] [varchar](40) NULL,
	[CorporateCity] [varchar](40) NULL,
	[CorporateState] [char](2) NULL,
	[CorporateZip] [char](5) NULL,
	[CorporateZip4] [char](4) NULL,
	[TaxIDNumber] [char](10) NULL,
	[BillingAddressLine1] [varchar](40) NULL,
	[BillingAddressLine2] [varchar](40) NULL,
	[BillingCity] [varchar](40) NULL,
	[BillingState] [char](2) NULL,
	[BillingZip] [char](5) NULL,
	[BillingZip4] [char](4) NULL,
	[Number] [char](3) NULL,
	[SalesRepClientID] [int] NULL,
	[Status] [char](1) NULL,
	[Approved] [bit] NULL,
	[ApprovedDate] [datetime] NULL,
	[ApprovedUser] [varchar](50) NULL,
	[LastUpdated] [datetime] NOT NULL,
	[UserID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ltlClient] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ltlClient] ADD  CONSTRAINT [DF_ltlClient_ContactName]  DEFAULT ('') FOR [ContactName]
GO

ALTER TABLE [dbo].[ltlClient] ADD  CONSTRAINT [DF_ltlClient_ContactPhone]  DEFAULT ('') FOR [ContactPhone]
GO

ALTER TABLE [dbo].[ltlClient] ADD  CONSTRAINT [DF_ltlClient_ContactEmail]  DEFAULT ('') FOR [ContactEmail]
GO

ALTER TABLE [dbo].[ltlClient] ADD  CONSTRAINT [DF_ltlClient_Status]  DEFAULT ('I') FOR [Status]
GO

ALTER TABLE [dbo].[ltlClient] ADD  CONSTRAINT [DF_ltlClient_LastUpdated]  DEFAULT (getdate()) FOR [LastUpdated]
GO

ALTER TABLE [dbo].[ltlClient] ADD  CONSTRAINT [DF_ltlClient_UserID]  DEFAULT (suser_sname()) FOR [UserID]
GO
