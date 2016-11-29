USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLQuoteCreate]    Script Date: 03/03/2014 11:12:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLQuoteCreate] 
    @ShipDate DATETIME,
    @OriginZip CHAR(5),
    @DestinationZip CHAR(5),
    @InsidePickup BIT,
    @InsideDelivery BIT,
    @AppointmentOrigin BIT,
    @AppointmentDestination BIT,
    @LiftGateOrigin BIT,
    @LiftGateDestination BIT,
    @Pallet1InsuranceValue DECIMAL(9,2),
    @Pallet2InsuranceValue DECIMAL(9,2),
    @Pallet3InsuranceValue DECIMAL(9,2),
    @Pallet4InsuranceValue DECIMAL(9,2),
    @Pallet5InsuranceValue DECIMAL(9,2),
    @NumberOfPallets INT
AS
BEGIN
	SET NOCOUNT ON;

	EXEC dbo.uspLTLQuoteCreate @ShipDate, @OriginZip, @DestinationZip, @InsidePickup, @InsideDelivery, @AppointmentOrigin, @AppointmentDestination, @LiftGateOrigin, @LiftGateDestination, @Pallet1InsuranceValue, @Pallet2InsuranceValue, @Pallet3InsuranceValue, @Pallet4InsuranceValue, @Pallet5InsuranceValue, @NumberOfPallets
END

GO