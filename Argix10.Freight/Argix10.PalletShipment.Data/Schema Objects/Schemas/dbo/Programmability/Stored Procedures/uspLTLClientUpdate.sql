USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLClientUpdate]    Script Date: 03/03/2014 11:06:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLClientUpdate] 
    @ID	int
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
   ,@LastUpdated datetime
   ,@UserID varchar(50)
AS
BEGIN
	UPDATE	[Tsort].[dbo].[ltlClient] 
    SET		AddressLine1=@AddressLine1, AddressLine2=@AddressLine2, City=@City, [State]=@State, Zip=@Zip, Zip4=@Zip4, 
			ContactName=@ContactName, ContactPhone=@ContactPhone, ContactEmail=@ContactEmail, 
			CorporateName=@CorporateName, CorporateAddressLine1=@CorporateAddressLine1, CorporateAddressLine2=@CorporateAddressLine2, CorporateCity=@CorporateCity, [CorporateState]=@CorporateState, CorporateZip=@CorporateZip, CorporateZip4=@CorporateZip4, TaxIDNumber=@TaxIDNumber, 
			BillingAddressLine1=@BillingAddressLine1, BillingAddressLine2=@BillingAddressLine2, BillingCity=@BillingCity, [BillingState]=@BillingState, BillingZip=@BillingZip, BillingZip4=@BillingZip4, 
			LastUpdated=@LastUpdated, UserID=@UserID
	WHERE	ID = @ID
END

GO
