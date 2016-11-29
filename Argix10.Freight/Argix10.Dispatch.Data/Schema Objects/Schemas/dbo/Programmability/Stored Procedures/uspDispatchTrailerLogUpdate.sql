USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchTrailerLogUpdate]    Script Date: 03/03/2014 11:33:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[uspDispatchTrailerLogUpdate] 
	@ID int, 
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
	@LastUpdated datetime,
	@UserID varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

UPDATE	Tsort.dbo.dspTrailerLog
   SET	ScheduleDate=@ScheduleDate, TrailerNumber=@TrailerNumber,
		InboundDate=@InboundDate, InboundCarrier=@InboundCarrier, InboundSeal=@InboundSeal, InboundDriverName=@InboundDriverName, TDSNumber=@TDSNumber,
		InitialYardLocation=@InitialYardLocation,
		OutboundDate=@OutboundDate, OutboundCarrier=@OutboundCarrier, OutboundSeal=@OutboundSeal, OutboundDriverName=@OutboundDriverName, BOLNumber=@BOLNumber,
		Comments=@Comments, LastUpdated=@LastUpdated, UserID=@UserID
 WHERE	ID=@ID
END





GO


