USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchClientInboundScheduleInsert]    Script Date: 03/03/2014 11:31:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[uspDispatchClientInboundScheduleInsert] 
	@Created datetime,
	@CreateUserID varchar(50),
	@ScheduleDate datetime,
	@VendorName varchar(50),
	@ConsigneeName varchar(50),
	@CarrierName varchar(50),
	@DriverName varchar(50),
	@TrailerNumber varchar(15),
	@ScheduledArrival datetime,
	@IsLiveUnload bit,
	@Amount int,
	@AmountType varchar(25),
	@FreightType varchar(10),
	@SortDate datetime,
	@Comments varchar(50),
	@IsTemplate bit,
	@LastUpdated datetime,
	@UserID varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Tsort.dbo.dspClientInboundSheet (
			Created, CreateUserID, ScheduleDate, VendorName, ConsigneeName, ScheduledArrival, 
			CarrierName, DriverName, TrailerNumber, 
			Amount, AmountType, FreightType, SortDate, Comments, 
			IsLiveUnload, IsTemplate, LastUpdated, UserID )
     VALUES (
			@Created, @CreateUserID, @ScheduleDate, @VendorName, @ConsigneeName, @ScheduledArrival, 
			@CarrierName, @DriverName, @TrailerNumber, 
			@Amount, @AmountType, @FreightType, @SortDate, @Comments,
			@IsLiveUnload, @IsTemplate, @LastUpdated, @UserID )
END


GO


