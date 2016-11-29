USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchPickupLogRequestUpdate]    Script Date: 03/03/2014 11:33:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspDispatchPickupLogRequestUpdate] 
	@RequestID int, 
	@ShipperNumber varchar(14)=null,
	@Shipper varchar(50)=null,
	@ShipperAddress varchar(50)=null,
	@ShipperPhone varchar(50)=null,
	@WindowOpen smallint=null,
	@WindowClose smallint=null,
	@DriverName varchar(50)=null,
	@ActualPickup datetime=null,
	@OrderType varchar(1)=null,
	@LastUpdated datetime,
	@UserID varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE	Tsort.dbo.dspPickupLog
	   SET	ShipperNumber=isnull(@ShipperNumber,ShipperNumber), Shipper=isnull(@Shipper,Shipper), ShipperAddress=isnull(@ShipperAddress,ShipperAddress), 
			ShipperPhone=isnull(@ShipperPhone,ShipperPhone), WindowOpen=isnull(@WindowOpen,WindowOpen), WindowClose=isnull(@WindowClose,WindowClose), 
			DriverName=isnull(@DriverName,DriverName), ActualPickup=isnull(@ActualPickup,ActualPickup), 
			LastUpdated=@LastUpdated, UserID=@UserID
	 WHERE	RequestID=@RequestID
END

GO


