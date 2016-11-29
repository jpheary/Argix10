USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchPickupLogRead]    Script Date: 03/03/2014 11:33:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[uspDispatchPickupLogRead]
	@RequestID int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	RequestID,Created,CreateUserID,ScheduleDate,
			CallerName,ClientNumber,Client,ShipperNumber,Shipper,ShipperAddress,ShipperPhone,
			WindowOpen,WindowClose,
			TerminalNumber,Terminal,DriverName,ActualPickup,OrderType,
			Amount,AmountType,FreightType,Weight,
			Comments,IsTemplate,Cancelled,CancelledUserID,LastUpdated,UserID,RowVersion
	FROM	dspPickupLog
	WHERE	RequestID = @RequestID
END




GO


