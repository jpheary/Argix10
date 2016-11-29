USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLShipperCreate]    Script Date: 03/03/2014 11:18:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLShipperCreate] 
	@ID	int OUTPUT
   ,@ClientID int
   ,@Name varchar(40)
   ,@AddressLine1 varchar(40)
   ,@AddressLine2 varchar(40)
   ,@City varchar(40) 
   ,@State char(2)
   ,@Zip char(5)
   ,@Zip4 char(4)
   ,@WindowStartTime datetime
   ,@WindowEndTime datetime
   ,@ContactName varchar(40)
   ,@ContactPhone varchar(24)
   ,@ContactEmail varchar(50)
   ,@Status char(1)
   ,@LastUpdated datetime
   ,@UserID varchar(50)
AS
BEGIN
	INSERT INTO [Tsort].[dbo].[ltlShipper] (
			ClientID, 
			Name, AddressLine1, AddressLine2, City, [State], Zip, Zip4, 
			WindowStartTime, WindowEndTime, ContactName, ContactPhone, ContactEmail, 
			[Status], LastUpdated, UserID )
    VALUES (
			@ClientID, 
			@Name, @AddressLine1, @AddressLine2, @City, @State, @Zip, @Zip4, 
			@WindowStartTime, @WindowEndTime, @ContactName, @ContactPhone, @ContactEmail, 
			@Status, @LastUpdated, @UserID )
	SET @ID = @@Identity
END

GO