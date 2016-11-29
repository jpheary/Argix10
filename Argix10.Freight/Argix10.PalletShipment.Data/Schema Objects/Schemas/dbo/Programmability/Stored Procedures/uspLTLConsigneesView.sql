USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLConsigneesView]    Script Date: 03/03/2014 11:09:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLConsigneesView] 
	@ClientID int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Co.[ID]
		  ,Co.[ClientID]
		  ,Cl.Name AS ClientName
		  ,Co.[Name]
		  ,Co.[AddressLine1]
		  ,Co.[AddressLine2]
		  ,Co.[City]
		  ,Co.[State]
		  ,Co.[Zip]
		  ,Co.[Zip4]
		  ,Co.[WindowStartTime]
		  ,Co.[WindowEndTime]
		  ,Co.[ContactName]
		  ,Co.[ContactPhone]
		  ,Co.[ContactEmail]
		  ,Co.[Status]
		  ,Co.[LastUpdated]
		  ,Co.[UserID]
	  FROM [Tsort].[dbo].[ltlConsignee] AS Co
	  LEFT JOIN [Tsort].[dbo].[ltlClient] AS Cl ON Co.ClientID = Cl.ID 
	  WHERE (@ClientID = 0 OR Co.ClientID = @ClientID)
  END

GO