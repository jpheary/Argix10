USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchOutboundScheduleTemplatesView]    Script Date: 03/03/2014 11:32:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[uspDispatchOutboundScheduleTemplatesView]
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
	WHERE	Cancelled IS NULL AND IsTemplate = '1'
END



GO


