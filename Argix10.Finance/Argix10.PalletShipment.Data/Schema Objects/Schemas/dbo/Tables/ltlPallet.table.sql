USE [Tsort]
GO

/****** Object:  Table [dbo].[ltlPallet]    Script Date: 03/03/2014 10:50:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ltlPallet](
	[ID] [int] IDENTITY(100000,1) NOT NULL,
	[PalletNumber] [varchar](11) NOT NULL,
	[ShipmentID] [int] NOT NULL,
	[Weight] [int] NOT NULL,
	[NMFCClass] [varchar](5) NULL,
	[InsuranceValue] [decimal](9, 2) NULL,
 CONSTRAINT [PK_ltlPallet] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ltlPallet]  WITH NOCHECK ADD  CONSTRAINT [FK_ltlPallet_ltlShipment] FOREIGN KEY([ShipmentID])
REFERENCES [dbo].[ltlShipment] ([ID])
GO

ALTER TABLE [dbo].[ltlPallet] CHECK CONSTRAINT [FK_ltlPallet_ltlShipment]
GO

ALTER TABLE [dbo].[ltlPallet] ADD  CONSTRAINT [DF_ltlPallet_NMFCClass]  DEFAULT ('FAK') FOR [NMFCClass]
GO


