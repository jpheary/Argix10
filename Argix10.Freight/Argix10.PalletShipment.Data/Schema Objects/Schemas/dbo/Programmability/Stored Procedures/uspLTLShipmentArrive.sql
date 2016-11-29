USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLShipmentArrive]    Script Date: 03/03/2014 11:16:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLShipmentArrive] 
    @ID	int,
    @PickupDate datetime
AS
BEGIN	
	UPDATE	[Tsort].[dbo].[ltlShipment] 
    SET		PickupDate = @PickupDate
	WHERE	ID = @ID
END

GO