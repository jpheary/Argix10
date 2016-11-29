USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchOutboundScheduleView]    Script Date: 03/03/2014 11:32:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[uspDispatchOutboundScheduleView]
	@StartDate datetime,
	@EndDate datetime
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	ID,Created,CreateUserID,ScheduleDate,
			Origin,OriginLocation,Destination,DestinationLocation,
			CarrierName, DriverName,TrailerNumber,DropEmptyTrailerNumber,
			ScheduledDeparture,ActualDeparture,ScheduledArrival,ActualArrival,Confirmed,
			Amount,AmountType,FreightType,
			Comments,IsTemplate,CancelledUserID,Cancelled,
			LastUpdated,UserID,RowVersion 
	FROM	dspOutboundSchedule
	WHERE	ScheduleDate >= @StartDate AND ScheduleDate <= @EndDate AND Cancelled IS NULL AND (IsTemplate IS NULL OR IsTemplate = '0')
END



GO


