USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLShipperRead]    Script Date: 03/03/2014 11:18:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLShipperRead] 
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
	  FROM [Tsort].[dbo].[ltlShipper]
	  WHERE ID = @ID
  END

GO