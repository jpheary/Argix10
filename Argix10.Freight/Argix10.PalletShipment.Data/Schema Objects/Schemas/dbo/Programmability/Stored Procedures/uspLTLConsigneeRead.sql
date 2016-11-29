USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLConsigneeRead]    Script Date: 03/03/2014 11:09:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLConsigneeRead] 
	@ID int
	
AS
BEGIN
	SELECT [ID]
		  ,[ClientID]
		  ,[Name]
		  ,[AddressLine1]
		  ,[AddressLine2]
		  ,[City]
		  ,[State]
		  ,[Zip]
		  ,[Zip4]
		  ,[WindowStartTime]
		  ,[WindowEndTime]
		  ,[ContactName]
		  ,[ContactPhone]
		  ,[ContactEmail]
		  ,[Status]
		  ,[LastUpdated]
		  ,[UserID]
	  FROM [Tsort].[dbo].[ltlConsignee]
	  WHERE ID = @ID
  END

GO