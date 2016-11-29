USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchClientInboundScheduleView]    Script Date: 03/03/2014 11:31:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[uspDispatchClientInboundScheduleView]
	@StartDate datetime,
	@EndDate datetime
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	ID,Created,CreateUserID,ScheduleDate,
			VendorName,ConsigneeName,
			CarrierName, DriverName,TrailerNumber,
			ScheduledArrival,ActualArrival,IsLiveUnload,
			Amount,AmountType,FreightType,SortDate,TDSNumber,TDSCreateUserID,
			Comments,IsTemplate,CancelledUserID,Cancelled,
			LastUpdated,UserID,RowVersion 
	FROM	dspClientInboundSheet
	WHERE	ScheduleDate >= @StartDate AND  ScheduleDate <= @EndDate AND Cancelled IS NULL AND (IsTemplate IS NULL OR IsTemplate = '0')
END



GO


