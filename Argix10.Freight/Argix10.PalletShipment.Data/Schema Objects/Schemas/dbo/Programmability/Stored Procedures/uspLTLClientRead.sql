USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLClientRead]    Script Date: 03/03/2014 11:05:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLClientRead] 
	@ID int
	
AS
BEGIN
	SELECT [ID]
		  ,[Name]
		  ,[AddressLine1]
		  ,[AddressLine2]
		  ,[City]
		  ,[State]
		  ,[Zip]
		  ,[Zip4]
		  ,[ContactName]
		  ,[ContactPhone]
		  ,[ContactEmail]
		  ,[CorporateName]
		  ,[CorporateAddressLine1]
		  ,[CorporateAddressLine2]
		  ,[CorporateCity]
		  ,[CorporateState]
		  ,[CorporateZip]
		  ,[CorporateZip4]
		  ,[TaxIDNumber]
		  ,[BillingAddressLine1]
		  ,[BillingAddressLine2]
		  ,[BillingCity]
		  ,[BillingState]
		  ,[BillingZip]
		  ,[BillingZip4]
		  ,[Approved]
		  ,[ApprovedDate]
		  ,[ApprovedUser]
		  ,[Status]
		  ,[LastUpdated]
		  ,[UserID]
		  ,[Number]
		  ,[SalesRepClientID]
	  FROM [Tsort].[dbo].[ltlClient]
	  WHERE ID = @ID
  END

GO


