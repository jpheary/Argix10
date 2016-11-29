USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchRptPickupLogView]    Script Date: 03/03/2014 11:33:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[uspDispatchRptPickupLogView]
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
	WHERE	ScheduleDate >= @StartDate AND ScheduleDate <= @EndDate AND (CreateUserID = 'PSP')
	ORDER BY ScheduleDate DESC
END




GO


