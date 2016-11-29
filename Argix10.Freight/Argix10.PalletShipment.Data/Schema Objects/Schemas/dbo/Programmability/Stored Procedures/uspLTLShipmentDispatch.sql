USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLShipmentDispatch]    Script Date: 03/03/2014 11:16:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLShipmentDispatch] 
    @ID	int,
    @PickupID int
AS
BEGIN	
	UPDATE	[Tsort].[dbo].[ltlShipment] 
    SET		PickupID = @PickupID
	WHERE	ID = @ID
END

GO