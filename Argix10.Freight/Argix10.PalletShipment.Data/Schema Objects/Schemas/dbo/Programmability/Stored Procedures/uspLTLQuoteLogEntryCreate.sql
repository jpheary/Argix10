USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLQuoteLog2EntryCreate]    Script Date: 03/03/2014 11:13:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLQuoteLogEntryCreate] 
    @Created datetime
   ,@ShipDate datetime
   ,@OriginZip varchar(5)
   ,@DestinationZip varchar(5)
   ,@Pallet1Weight int
   ,@Pallet1Class varchar(5)
   ,@Pallet1InsuranceValue Decimal(9,2)
   ,@Pallet2Weight int
   ,@Pallet2Class varchar(5)
   ,@Pallet2InsuranceValue Decimal(9,2)
   ,@Pallet3Weight int
   ,@Pallet3Class varchar(5)
   ,@Pallet3InsuranceValue Decimal(9,2)
   ,@Pallet4Weight int
   ,@Pallet4Class varchar(5)
   ,@Pallet4InsuranceValue Decimal(9,2)
   ,@Pallet5Weight int
   ,@Pallet5Class varchar(5)
   ,@Pallet5InsuranceValue Decimal(9,2)
   ,@InsidePickup bit
   ,@LiftGateOrigin bit
   ,@AppointmentOrigin bit
   ,@InsideDelivery bit
   ,@LiftGateDestination bit
   ,@AppointmentDestination bit
   ,@Pallets int
   ,@Weight decimal(9,2)
   ,@PalletRate decimal(9,2)
   ,@FuelSurcharge decimal(9,2)
   ,@AccessorialCharge decimal(9,2)
   ,@InsuranceCharge decimal(9,2)
   ,@TollCharge decimal(9,2)
   ,@TotalCharge decimal(9,2)
AS
BEGIN
	INSERT INTO [Tsort].[dbo].[ltlQuoteLog] (
			[Created],[ShipDate],[OriginZip],[DestinationZip]
           ,[Pallet1Weight],[Pallet1Class],[Pallet1InsuranceValue],[Pallet2Weight],[Pallet2Class],[Pallet2InsuranceValue],[Pallet3Weight],[Pallet3Class],[Pallet3InsuranceValue],[Pallet4Weight],[Pallet4Class],[Pallet4InsuranceValue],[Pallet5Weight],[Pallet5Class],[Pallet5InsuranceValue]
           ,[InsidePickup],[LiftGateOrigin],[AppointmentOrigin],[InsideDelivery],[LiftGateDestination],[AppointmentDestination]
           ,[Pallets],[Weight],[PalletRate],[FuelSurcharge],[AccessorialCharge],[InsuranceCharge],[TollCharge],[TotalCharge] )
    VALUES (
			@Created,@ShipDate,@OriginZip,@DestinationZip
           ,@Pallet1Weight,@Pallet1Class,@Pallet1InsuranceValue,@Pallet2Weight,@Pallet2Class,@Pallet2InsuranceValue,@Pallet3Weight,@Pallet3Class,@Pallet3InsuranceValue,@Pallet4Weight,@Pallet4Class,@Pallet4InsuranceValue,@Pallet5Weight,@Pallet5Class,@Pallet5InsuranceValue
           ,@InsidePickup,@LiftGateOrigin,@AppointmentOrigin,@InsideDelivery,@LiftGateDestination,@AppointmentDestination
           ,@Pallets,@Weight,@PalletRate,@FuelSurcharge,@AccessorialCharge,@InsuranceCharge,@TollCharge,@TotalCharge )
END

GO