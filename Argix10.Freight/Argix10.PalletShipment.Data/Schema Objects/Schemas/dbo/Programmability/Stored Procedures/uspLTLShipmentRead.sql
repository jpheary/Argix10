USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLShipmentRead]    Script Date: 03/03/2014 11:16:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLShipmentRead] 
	@ID int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [ID]
		  ,[ShipmentNumber]
		  ,[Created]
		  ,[ClientID]
		  ,[ShipDate]
		  ,[ShipperID]
		  ,[ConsigneeID]
		  ,[Pallets]
		  ,[Weight]
		  ,[PalletRate]
		  ,[FuelSurcharge]
		  ,[AccessorialCharge]
		  ,[InsuranceCharge]
		  ,[TollCharge]
		  ,[TotalCharge]
		  ,[InsidePickup]
		  ,[LiftGateOrigin]
		  ,[AppointmentOrigin]
		  ,[InsideDelivery]
		  ,[LiftGateDestination]
		  ,[AppointmentDestination]
		  ,[LastUpdated]
		  ,[UserID]
		  ,[PickupID]
		  ,[PickupDate]
	  FROM [Tsort].[dbo].[ltlShipment]
	  WHERE (ID = @ID)
  END

GO