USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLShipmentCreate]    Script Date: 03/03/2014 11:16:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLShipmentCreate] 
    @ID	int OUTPUT
   ,@Created datetime
   ,@ClientID int
   ,@ShipDate datetime
   ,@ShipperID int
   ,@ConsigneeID int
   ,@Pallets int
   ,@Weight decimal(9,2)
   ,@PalletRate decimal(9,2)
   ,@FuelSurcharge decimal(9,2)
   ,@AccessorialCharge decimal(9,2)
   ,@InsuranceCharge decimal(9,2)
   ,@TollCharge decimal(9,2)
   ,@TotalCharge decimal(9,2)
   ,@InsidePickup bit
   ,@LiftGateOrigin bit
   ,@AppointmentOrigin datetime
   ,@InsideDelivery bit
   ,@LiftGateDestination bit
   ,@AppointmentDestination datetime
   ,@LastUpdated datetime
   ,@UserID varchar(50)
AS
BEGIN
	INSERT INTO [Tsort].[dbo].[ltlShipment] (
			ShipmentNumber, Created, ClientID, ShipDate, ShipperID, ConsigneeID, 
			Pallets, [Weight], PalletRate, FuelSurcharge, AccessorialCharge, InsuranceCharge, TollCharge, TotalCharge, 
			InsidePickup, LiftGateOrigin, AppointmentOrigin, InsideDelivery, LiftGateDestination, AppointmentDestination, 
			LastUpdated, UserID )
    VALUES (
			'000000000', @Created, @ClientID, @ShipDate, @ShipperID, @ConsigneeID, 
			@Pallets, @Weight, @PalletRate, @FuelSurcharge, @AccessorialCharge, @InsuranceCharge, @TollCharge, @TotalCharge, 
			@InsidePickup, @LiftGateOrigin, @AppointmentOrigin, @InsideDelivery, @LiftGateDestination, @AppointmentDestination, 
			@LastUpdated, @UserID )
	SET @ID = @@Identity
	
	UPDATE [Tsort].[dbo].[ltlShipment] SET ShipmentNumber = RIGHT('000000000' + CONVERT(varchar(9), @@Identity), 9) WHERE ID = @@Identity
END

GO