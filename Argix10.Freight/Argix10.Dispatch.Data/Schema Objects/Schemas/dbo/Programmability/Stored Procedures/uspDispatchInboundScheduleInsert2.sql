USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchInboundScheduleInsert2]    Script Date: 03/03/2014 11:32:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[uspDispatchInboundScheduleInsert2] 
	@Created datetime,
	@CreateUserID varchar(50),
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
	@ScheduledArrival datetime,
	@Confirmed bit,
	@Amount int, 
	@AmountType varchar(25),
	@FreightType varchar(10), 
	@Comments varchar(50),
	@IsTemplate bit,
	@LastUpdated datetime,
	@UserID varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Tsort.dbo.dspInboundSchedule (
			Created, CreateUserID, ScheduleDate, 
			Origin, OriginLocation, Destination, DestinationLocation, 
			CarrierName, DriverName, TrailerNumber, DropEmptyTrailerNumber,
			ScheduledDeparture, ScheduledArrival, Confirmed, 
			Amount, AmountType, FreightType, Comments, 
			IsTemplate, LastUpdated, UserID )
     VALUES (
			@Created, @CreateUserID, @ScheduleDate, 
			@Origin, @OriginLocation, @Destination, @DestinationLocation, 
			@CarrierName, @DriverName, @TrailerNumber, @DropEmptyTrailerNumber,
			@ScheduledDeparture, @ScheduledArrival, @Confirmed, 
			@Amount, @AmountType, @FreightType, @Comments, 
			@IsTemplate, @LastUpdated, @UserID )
END




GO


