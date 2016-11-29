USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchPickupLogUpdate]    Script Date: 03/03/2014 11:33:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[uspDispatchPickupLogUpdate] 
	@RequestID int, 
	@ScheduleDate datetime,
	@CallerName varchar(50),
	@ClientNumber varchar(3),
	@Client varchar(50),
	@ShipperNumber varchar(14),
	@Shipper varchar(50),
	@ShipperAddress varchar(50),
	@ShipperPhone varchar(50),
	@WindowOpen smallint,
	@WindowClose smallint,
	@TerminalNumber varchar(50),
	@Terminal varchar(50),
	@DriverName varchar(50),
	@ActualPickup datetime,
	@OrderType varchar(1),
	@Amount int,
	@AmountType varchar(50),
	@FreightType varchar(50),
	@Weight int,
	@Comments varchar(50),
	@LastUpdated datetime,
	@UserID varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

UPDATE	Tsort.dbo.dspPickupLog
   SET	ScheduleDate=@ScheduleDate, CallerName=@CallerName, ClientNumber=@ClientNumber, Client=@Client, 
		ShipperNumber=@ShipperNumber, Shipper=@Shipper, ShipperAddress=@ShipperAddress, ShipperPhone=@ShipperPhone, WindowOpen=@WindowOpen, WindowClose=@WindowClose, 
		TerminalNumber=@TerminalNumber, Terminal=@Terminal, DriverName=@DriverName, ActualPickup=@ActualPickup, OrderType=@OrderType, 
		Amount=@Amount, AmountType=@AmountType, FreightType=@FreightType, Weight=@Weight, 
		Comments=@Comments, LastUpdated=@LastUpdated, UserID=@UserID
 WHERE	RequestID=@RequestID
END



GO


