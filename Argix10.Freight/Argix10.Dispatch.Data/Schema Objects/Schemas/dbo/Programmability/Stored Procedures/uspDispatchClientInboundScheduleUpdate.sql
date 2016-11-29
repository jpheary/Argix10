USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchClientInboundScheduleUpdate]    Script Date: 03/03/2014 11:31:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[uspDispatchClientInboundScheduleUpdate] 
	@ID int, 
	@ScheduleDate datetime,
	@VendorName varchar(50),
	@ConsigneeName varchar(50),
	@CarrierName varchar(50),
	@DriverName varchar(50),
	@TrailerNumber varchar(15),
	@ScheduledArrival datetime,
	@ActualArrival datetime,
	@IsLiveUnload bit,
	@Amount int,
	@AmountType varchar(25),
	@FreightType varchar(10),
	@SortDate datetime,
	@TDSNumber varchar(15),
	@TDSCreateUserID varchar(50),
	@Comments varchar(50),
	@LastUpdated datetime,
	@UserID varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

UPDATE	Tsort.dbo.dspClientInboundSheet
   SET	ScheduleDate=@ScheduleDate, 
		VendorName=@VendorName, ConsigneeName=@ConsigneeName,
		CarrierName=@CarrierName, DriverName=@DriverName, TrailerNumber=@TrailerNumber, 
		ScheduledArrival=@ScheduledArrival, ActualArrival=@ActualArrival, 
		IsLiveUnload=@IsLiveUnload, 
		Amount=@Amount, AmountType=@AmountType, FreightType=@FreightType, 
		SortDate=@SortDate, TDSNumber=@TDSNumber, TDSCreateUserID=@TDSCreateUserID, 
		Comments=@Comments, LastUpdated=@LastUpdated, UserID=@UserID
 WHERE	ID = @ID
END


GO


