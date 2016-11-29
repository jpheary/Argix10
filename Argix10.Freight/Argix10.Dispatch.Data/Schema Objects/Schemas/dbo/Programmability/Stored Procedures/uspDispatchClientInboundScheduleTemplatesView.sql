USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchClientInboundScheduleTemplatesView]    Script Date: 03/03/2014 11:31:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[uspDispatchClientInboundScheduleTemplatesView]
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
	WHERE	Cancelled IS NULL AND IsTemplate = '1'
END



GO


