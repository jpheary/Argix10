USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchPickupLogCancel]    Script Date: 03/03/2014 11:32:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[uspDispatchPickupLogCancel]
	@RequestID int,
	@Cancelled datetime,
	@CancelledUserID varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

UPDATE	Tsort.dbo.dspPickupLog
   SET	Cancelled = @Cancelled, CancelledUserID = @CancelledUserID
 WHERE	RequestID = @RequestID
END



GO


