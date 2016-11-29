USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchTrailerLogCancel]    Script Date: 03/03/2014 11:33:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[uspDispatchTrailerLogCancel]
	@ID int,
	@Cancelled datetime,
	@CancelledUserID varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

UPDATE	Tsort.dbo.dspTrailerLog
   SET	Cancelled = @Cancelled, CancelledUserID = @CancelledUserID
 WHERE	ID = @ID
END




GO


