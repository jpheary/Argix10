USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchOutboundScheduleCancel]    Script Date: 03/03/2014 11:32:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[uspDispatchOutboundScheduleCancel]
	@ID int,
	@Cancelled datetime,
	@CancelledUserID varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

UPDATE	Tsort.dbo.dspOutboundSchedule
   SET	Cancelled = @Cancelled, CancelledUserID = @CancelledUserID
 WHERE	ID = @ID
END



GO


