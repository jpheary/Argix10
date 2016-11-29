USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchPickupLogInsert3]    Script Date: 03/03/2014 11:32:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspDispatchPickupLogInsert3] 
	@RequestID int OUTPUT, 
	@Created datetime,
	@CreateUserID varchar(50),
	@ScheduleDate datetime,
	@CallerName varchar(50),
	@ClientNumber varchar(3),
	@Client varchar(50),
	@ShipperNumber varchar(14),
	@Shipper varchar(50),
	@ShipperAddress varchar(50),
	@ShipperPhone varchar(50),
	@WindowOpen smallint,
	@WindowClose smallint,
	@TerminalNumber varchar(50),
	@Terminal varchar(50),
	@DriverName varchar(50),
	@OrderType varchar(1),
	@Amount int,
	@AmountType varchar(50),
	@FreightType varchar(50),
	@Weight int,
	@Comments varchar(50),
	@IsTemplate bit,
	@LastUpdated datetime,
	@UserID varchar(50)
AS
BEGIN

	INSERT INTO Tsort.dbo.dspPickupLog (
			Created, CreateUserID, ScheduleDate, CallerName, ClientNumber, Client, 
			ShipperNumber, Shipper, ShipperAddress, ShipperPhone, WindowOpen, WindowClose, 
			TerminalNumber, Terminal, DriverName, OrderType, 
			Amount, AmountType, FreightType, Weight, 
			Comments, IsTemplate, LastUpdated, UserID )
     VALUES (
           @Created, @CreateUserID, @ScheduleDate, @CallerName, @ClientNumber, @Client, 
           @ShipperNumber, @Shipper, @ShipperAddress, @ShipperPhone, @WindowOpen, @WindowClose, 
           @TerminalNumber, @Terminal, @DriverName, @OrderType, 
           @Amount, @AmountType, @FreightType, @Weight, 
           @Comments, @IsTemplate, @LastUpdated, @UserID )


	SET @RequestID = @@Identity
END


GO


