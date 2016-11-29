USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLShipperUpdate]    Script Date: 03/03/2014 11:18:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLShipperUpdate] 
	@ID	int
   ,@AddressLine1 varchar(40)
   ,@AddressLine2 varchar(40)
   ,@WindowStartTime datetime
   ,@WindowEndTime datetime
   ,@ContactName varchar(40)
   ,@ContactPhone varchar(24)
   ,@ContactEmail varchar(50)
   ,@LastUpdated datetime
   ,@UserID varchar(50)
AS
BEGIN	
	UPDATE	[Tsort].[dbo].[ltlShipper] 
    SET		AddressLine1=@AddressLine1, AddressLine2=@AddressLine2, 
			WindowStartTime=@WindowStartTime, WindowEndTime=@WindowEndTime, ContactName=@ContactName, ContactPhone=@ContactPhone, ContactEmail=@ContactEmail, 
			LastUpdated=@LastUpdated, UserID=@UserID
	WHERE	ID = @ID
END

GO