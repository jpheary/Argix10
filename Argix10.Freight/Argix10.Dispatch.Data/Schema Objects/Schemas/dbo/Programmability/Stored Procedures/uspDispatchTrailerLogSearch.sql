USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchTrailerLogSearch]    Script Date: 03/03/2014 11:33:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[uspDispatchTrailerLogSearch]
	@TrailerNumber varchar(15)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	ID,Created,CreateUserID,ScheduleDate,TrailerNumber,
			InboundDate,InboundCarrier,InboundSeal,InboundDriverName,TDSNumber,InitialYardLocation,
			OutboundDate,OutboundCarrier,OutboundSeal,OutboundDriverName,BOLNumber,
			Comments,IsTemplate,CancelledUserID,Cancelled,
			LastUpdated,UserID,RowVersion 
	FROM	dspTrailerLog
	WHERE	TrailerNumber = @TrailerNumber AND Cancelled IS NULL AND (IsTemplate IS NULL OR IsTemplate = '0')
END







GO


