USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchPickupLogViewByClient]    Script Date: 03/03/2014 11:33:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspDispatchPickupLogViewByClient]
	@StartDate datetime,
	@EndDate datetime,
	@ClientNumber varchar(3)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	RequestID,Created,CreateUserID,ScheduleDate,
			CallerName,ClientNumber,Client,
			ShipperNumber,Shipper,ShipperAddress,ShipperPhone,
			WindowOpen,WindowClose,
			TerminalNumber,Terminal,DriverName,ActualPickup,OrderType,
			Amount,AmountType,FreightType,Weight,
			Comments,IsTemplate,
			Cancelled,CancelledUserID,LastUpdated,UserID,RowVersion
	FROM	dspPickupLog
	WHERE	ScheduleDate >= @StartDate AND  ScheduleDate <= @EndDate AND ClientNumber = @ClientNumber
	ORDER BY ScheduleDate DESC
END

GO


