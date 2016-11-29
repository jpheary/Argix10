USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchInboundScheduleCancel]    Script Date: 03/03/2014 11:32:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[uspDispatchInboundScheduleCancel]
	@ID int,
	@Cancelled datetime,
	@CancelledUserID varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

UPDATE	Tsort.dbo.dspInboundSchedule
   SET	Cancelled = @Cancelled, CancelledUserID = @CancelledUserID
 WHERE	ID = @ID
END



GO


