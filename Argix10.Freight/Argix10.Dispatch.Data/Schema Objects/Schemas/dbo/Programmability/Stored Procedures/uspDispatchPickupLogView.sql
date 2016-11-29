USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchPickupLogView]    Script Date: 03/03/2014 11:33:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[uspDispatchPickupLogView]
	@StartDate datetime,
	@EndDate datetime
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
	WHERE	ScheduleDate >= @StartDate AND ScheduleDate <= @EndDate AND Cancelled IS NULL AND (IsTemplate IS NULL OR IsTemplate = '0')
END



GO


