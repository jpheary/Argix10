USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLShipmentsView]    Script Date: 03/03/2014 11:16:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLShipmentsView] 
	@ClientID int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT S.[ID]
		  ,S.[ShipmentNumber]
		  ,S.[Created]
		  ,S.[ClientID]
		  ,Cl.Name AS ClientName
		  ,S.[ShipDate]
		  ,S.[ShipperID]
		  ,Sh.Name AS ShipperName
		  ,S.[ConsigneeID]
		  ,Co.Name AS ConsigneeName
		  ,S.[Pallets]
		  ,S.[Weight]
		  ,S.[PalletRate]
		  ,S.[FuelSurcharge]
		  ,S.[AccessorialCharge]
		  ,S.[InsuranceCharge]
		  ,S.[TollCharge]
		  ,S.[TotalCharge]
		  ,S.[InsidePickup]
		  ,S.[LiftGateOrigin]
		  ,S.[AppointmentOrigin]
		  ,S.[InsideDelivery]
		  ,S.[LiftGateDestination]
		  ,S.[AppointmentDestination]
		  ,S.[LastUpdated]
		  ,S.[UserID]
		  ,S.[PickupID]
		  ,S.[PickupDate]
	  FROM [Tsort].[dbo].[ltlShipment] AS S
	  LEFT JOIN [Tsort].[dbo].[ltlClient] AS Cl ON S.clientID = Cl.ID 
	  LEFT JOIN [Tsort].[dbo].[ltlShipper] AS Sh ON S.ShipperID = Sh.ID 
	  LEFT JOIN [Tsort].[dbo].[ltlConsignee] AS Co ON S.ConsigneeID = Co.ID 
	  WHERE (@ClientID = 0 OR S.ClientID = @ClientID)
	  ORDER BY [ID] DESC
  END

GO