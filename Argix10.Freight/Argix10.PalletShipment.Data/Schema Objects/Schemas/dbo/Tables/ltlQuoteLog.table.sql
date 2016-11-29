CREATE TABLE [dbo].[ltlQuoteLog](
	[ID] [int] IDENTITY(100000,1) NOT NULL,
	[Created] [datetime] NOT NULL,
	[ShipDate] [datetime] NOT NULL,
	[OriginZip] [varchar](5) NOT NULL,
	[DestinationZip] [varchar](5) NOT NULL,
	[Pallet1Weight] [int] NOT NULL,
	[Pallet1Class] [varchar](5) NOT NULL,
	[Pallet1InsuranceValue] [decimal](9, 2) NULL,
	[Pallet2Weight] [int] NULL,
	[Pallet2Class] [varchar](5) NULL,
	[Pallet2InsuranceValue] [decimal](9, 2) NULL,
	[Pallet3Weight] [int] NULL,
	[Pallet3Class] [varchar](5) NULL,
	[Pallet3InsuranceValue] [decimal](9, 2) NULL,
	[Pallet4Weight] [int] NULL,
	[Pallet4Class] [varchar](5) NULL,
	[Pallet4InsuranceValue] [decimal](9, 2) NULL,
	[Pallet5Weight] [int] NULL,
	[Pallet5Class] [varchar](5) NULL,
	[Pallet5InsuranceValue] [decimal](9, 2) NULL,
	[InsidePickup] [bit] NULL,
	[LiftGateOrigin] [bit] NULL,
	[AppointmentOrigin] [bit] NULL,
	[InsideDelivery] [bit] NULL,
	[LiftGateDestination] [bit] NULL,
	[AppointmentDestination] [bit] NULL,
	[Pallets] [int] NOT NULL,
	[Weight] [decimal](9, 2) NOT NULL,
	[PalletRate] [decimal](9, 2) NOT NULL,
	[FuelSurcharge] [decimal](9, 2) NOT NULL,
	[AccessorialCharge] [decimal](9, 2) NOT NULL,
	[InsuranceCharge] [decimal](9, 2) NOT NULL,
	[TollCharge] [decimal](9, 2) NOT NULL,
	[TotalCharge] [decimal](9, 2) NOT NULL,
 CONSTRAINT [PK_ltlQuoteLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ltlQuoteLog] ADD  CONSTRAINT [DF_ltlQuoteLog_Created]  DEFAULT (getdate()) FOR [Created]
GO


