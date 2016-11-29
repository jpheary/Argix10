USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchPickupLogTemplatesView]    Script Date: 03/03/2014 11:33:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[uspDispatchPickupLogTemplatesView]
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
	WHERE	Cancelled IS NULL AND IsTemplate = '1'
END


GO


