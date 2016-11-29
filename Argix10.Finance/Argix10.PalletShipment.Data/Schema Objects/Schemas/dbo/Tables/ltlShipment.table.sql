USE [Tsort]
GO

/****** Object:  Table [dbo].[ltlShipment]    Script Date: 03/03/2014 10:51:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ltlShipment](
	[ID] [int] IDENTITY(100000,1) NOT NULL,
	[ShipmentNumber] [varchar](9) NOT NULL,
	[Created] [datetime] NOT NULL,
	[ClientID] [int] NOT NULL,
	[ShipDate] [datetime] NOT NULL,
	[ShipperID] [int] NOT NULL,
	[ConsigneeID] [int] NOT NULL,
	[Pallets] [int] NOT NULL,
	[Weight] [decimal](9, 2) NOT NULL,
	[InsidePickup] [bit] NULL,
	[LiftGateOrigin] [bit] NULL,
	[AppointmentOrigin] [datetime] NULL,
	[InsideDelivery] [bit] NULL,
	[LiftGateDestination] [bit] NULL,
	[AppointmentDestination] [datetime] NULL,
	[PalletRate] [decimal](9, 2) NOT NULL,
	[FuelSurcharge] [decimal](9, 2) NOT NULL,
	[AccessorialCharge] [decimal](9, 2) NOT NULL,
	[InsuranceCharge] [decimal](9, 2) NOT NULL,
	[TollCharge] [decimal](9, 2) NOT NULL,
	[TotalCharge] [decimal](9, 2) NOT NULL,
	[PickupID] [int] NULL,
	[PickupDate] [datetime] NULL,
	[LastUpdated] [datetime] NOT NULL,
	[UserID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ltlShipment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ltlShipment]  WITH NOCHECK ADD  CONSTRAINT [FK_ltlShipment_ltlClient] FOREIGN KEY([ClientID])
REFERENCES [dbo].[ltlClient] ([ID])
GO

ALTER TABLE [dbo].[ltlShipment] CHECK CONSTRAINT [FK_ltlShipment_ltlClient]
GO

ALTER TABLE [dbo].[ltlShipment]  WITH NOCHECK ADD  CONSTRAINT [FK_ltlShipment_ltlConsignee] FOREIGN KEY([ConsigneeID])
REFERENCES [dbo].[ltlConsignee] ([ID])
GO

ALTER TABLE [dbo].[ltlShipment] CHECK CONSTRAINT [FK_ltlShipment_ltlConsignee]
GO

ALTER TABLE [dbo].[ltlShipment]  WITH NOCHECK ADD  CONSTRAINT [FK_ltlShipment_ltlShipper] FOREIGN KEY([ShipperID])
REFERENCES [dbo].[ltlShipper] ([ID])
GO

ALTER TABLE [dbo].[ltlShipment] CHECK CONSTRAINT [FK_ltlShipment_ltlShipper]
GO

ALTER TABLE [dbo].[ltlShipment] ADD  CONSTRAINT [DF_ltlShipment_LastUpdated]  DEFAULT (getdate()) FOR [LastUpdated]
GO

ALTER TABLE [dbo].[ltlShipment] ADD  CONSTRAINT [DF_ltlShipment_UserID]  DEFAULT (suser_sname()) FOR [UserID]
GO


