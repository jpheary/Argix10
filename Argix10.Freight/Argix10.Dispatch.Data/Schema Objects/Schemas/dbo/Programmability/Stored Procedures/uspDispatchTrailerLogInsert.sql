USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchTrailerLogInsert]    Script Date: 03/03/2014 11:33:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[uspDispatchTrailerLogInsert] 
	@Created datetime,
	@CreateUserID varchar(50),
	@ScheduleDate datetime,
	@TrailerNumber varchar(15),
	@InboundDate datetime,
	@InboundCarrier varchar(50),
	@InboundSeal varchar(50),
	@InboundDriverName varchar(50),
	@TDSNumber varchar(15),
	@InitialYardLocation varchar(50),
	@OutboundDate datetime,
	@OutboundCarrier varchar(50),
	@OutboundSeal varchar(50),
	@OutboundDriverName varchar(50),
	@BOLNumber varchar(15),
	@Comments varchar(50),
	@IsTemplate bit,
	@LastUpdated datetime,
	@UserID varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Tsort.dbo.dspTrailerLog (
			Created, CreateUserID, ScheduleDate, TrailerNumber,
			InboundDate, InboundCarrier, InboundSeal, InboundDriverName, TDSNumber, InitialYardLocation,
			OutboundDate, OutboundCarrier, OutboundSeal, OutboundDriverName, BOLNumber, 
			Comments, IsTemplate, LastUpdated, UserID )
     VALUES (
			@Created, @CreateUserID, @ScheduleDate, @TrailerNumber,
			@InboundDate, @InboundCarrier, @InboundSeal, @InboundDriverName, @TDSNumber, @InitialYardLocation,
			@OutboundDate, @OutboundCarrier, @OutboundSeal, @OutboundDriverName, @BOLNumber, 
			@Comments, @IsTemplate, @LastUpdated, @UserID )
END





GO


