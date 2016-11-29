USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchOutboundScheduleUpdate2]    Script Date: 03/03/2014 11:32:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[uspDispatchOutboundScheduleUpdate2] 
	@ID int, 
	@ScheduleDate datetime, 
	@Origin varchar(50),
	@OriginLocation varchar(50),
	@Destination varchar(50),
	@DestinationLocation varchar(50),
	@CarrierName varchar(50),
	@DriverName varchar(50),
	@TrailerNumber varchar(15),
	@DropEmptyTrailerNumber varchar(15),
	@ScheduledDeparture datetime,
	@ActualDeparture datetime,
	@ScheduledArrival datetime,
	@ActualArrival datetime,
	@Confirmed bit,
	@Amount int, 
	@AmountType varchar(25),
	@FreightType varchar(10), 
	@Comments varchar(50),
	@LastUpdated datetime,
	@UserID varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

UPDATE	Tsort.dbo.dspOutboundSchedule
   SET	ScheduleDate=@ScheduleDate, 
		Origin=@Origin, OriginLocation=@OriginLocation, Destination=@Destination, DestinationLocation=@DestinationLocation, 
		CarrierName=@CarrierName, DriverName=@DriverName, TrailerNumber=@TrailerNumber, DropEmptyTrailerNumber=@DropEmptyTrailerNumber, 
		ScheduledDeparture=@ScheduledDeparture, ActualDeparture=@ActualDeparture, ScheduledArrival=@ScheduledArrival, ActualArrival=@ActualArrival, 
		Confirmed=@Confirmed, Amount=@Amount, AmountType=@AmountType, FreightType=@FreightType, Comments=@Comments, 
		LastUpdated=@LastUpdated, UserID=@UserID
 WHERE	ID = @ID
END




GO


