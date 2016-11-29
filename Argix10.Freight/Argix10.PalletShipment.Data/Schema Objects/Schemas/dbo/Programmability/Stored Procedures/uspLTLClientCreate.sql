USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLClientCreate]    Script Date: 03/03/2014 11:05:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLClientCreate] 
    @ID	int OUTPUT
   ,@Name varchar(40)
   ,@AddressLine1 varchar(40)
   ,@AddressLine2 varchar(40)
   ,@City varchar(40) 
   ,@State char(2)
   ,@Zip char(5)
   ,@Zip4 char(4)
   ,@ContactName varchar(40)
   ,@ContactPhone varchar(24)
   ,@ContactEmail varchar(50)
   ,@CorporateName varchar(40)
   ,@CorporateAddressLine1 varchar(40)
   ,@CorporateAddressLine2 varchar(40)
   ,@CorporateCity varchar(40) 
   ,@CorporateState char(2)
   ,@CorporateZip char(5)
   ,@CorporateZip4 char(4)
   ,@TaxIDNumber char(10)
   ,@BillingAddressLine1 varchar(40)
   ,@BillingAddressLine2 varchar(40)
   ,@BillingCity varchar(40) 
   ,@BillingState char(2)
   ,@BillingZip char(5)
   ,@BillingZip4 char(4)
   ,@SalesRepClientID int
   ,@Status char(1)
   ,@LastUpdated datetime
   ,@UserID varchar(50)
AS
BEGIN
	INSERT INTO [Tsort].[dbo].[ltlClient] (
			Name, AddressLine1, AddressLine2, City, [State], Zip, Zip4, 
			ContactName, ContactPhone, ContactEmail, 
			CorporateName, CorporateAddressLine1, CorporateAddressLine2, CorporateCity, [CorporateState], CorporateZip, CorporateZip4, TaxIDNumber, 
			BillingAddressLine1, BillingAddressLine2, BillingCity, [BillingState], BillingZip, BillingZip4, 
			SalesRepClientID, [Status], LastUpdated, UserID )
    VALUES (
			@Name, @AddressLine1, @AddressLine2, @City, @State, @Zip, @Zip4, 
			@ContactName, @ContactPhone, @ContactEmail, 
			@CorporateName, @CorporateAddressLine1, @CorporateAddressLine2, @CorporateCity, @CorporateState, @CorporateZip, @CorporateZip4, @TaxIDNumber, 
			@BillingAddressLine1, @BillingAddressLine2, @BillingCity, @BillingState, @BillingZip, @BillingZip4, 
			@SalesRepClientID, @Status, @LastUpdated, @UserID )
	SET @ID = @@Identity
END

GO


