USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLPalletCreate]    Script Date: 03/03/2014 11:12:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLPalletCreate] 
    @ID	int OUTPUT
   ,@ShipmentID int
   ,@Weight int
   ,@InsuranceValue decimal(9,2)
AS
BEGIN
	INSERT INTO [Tsort].[dbo].[ltlPallet] ( 
			 PalletNumber, ShipmentID, [Weight], InsuranceValue )
    VALUES ( '00000000000', @ShipmentID, @Weight, @InsuranceValue )
	SET @ID = @@Identity
	
	DECLARE @PalletIndex int
	SET @PalletIndex = (SELECT COUNT(ID) FROM [Tsort].[dbo].[ltlPallet] WHERE ShipmentID = @ShipmentID)
	UPDATE [Tsort].[dbo].[ltlPallet] SET PalletNumber = (RIGHT('000000000' + CONVERT(varchar(9), @ShipmentID), 9) + RIGHT('00' + CONVERT(varchar(2), @PalletIndex), 2)) WHERE ID = @@Identity
END

GO