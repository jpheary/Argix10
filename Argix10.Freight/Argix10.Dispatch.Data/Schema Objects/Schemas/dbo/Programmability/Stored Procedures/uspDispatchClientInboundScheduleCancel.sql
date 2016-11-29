USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchClientInboundScheduleCancel]    Script Date: 03/03/2014 11:31:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[uspDispatchClientInboundScheduleCancel]
	@ID int,
	@Cancelled datetime,
	@CancelledUserID varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

UPDATE	Tsort.dbo.dspClientInboundSheet
   SET	Cancelled = @Cancelled, CancelledUserID = @CancelledUserID
 WHERE	ID = @ID
END



GO


