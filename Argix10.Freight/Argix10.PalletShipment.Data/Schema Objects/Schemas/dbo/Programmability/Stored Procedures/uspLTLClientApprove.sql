USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLClientApprove]    Script Date: 03/03/2014 11:05:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLClientApprove] 
    @ID	int,
    @ClientNumber char(3),
    @Approved bit,
    @ApprovedUser varchar(50)
AS
BEGIN
	DECLARE @ApprovedDate datetime
	SET @ApprovedDate = Getdate()
	
	UPDATE	[Tsort].[dbo].[ltlClient] 
    SET		Number=@ClientNumber, Approved = @Approved, ApprovedDate = @ApprovedDate, ApprovedUser = @ApprovedUser
	WHERE	ID = @ID
END

GO


