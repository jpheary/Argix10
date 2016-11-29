USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLShippersView]    Script Date: 03/03/2014 11:18:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLShippersView] 
	@ClientID int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT S.[ID]
		  ,S.[ClientID]
		  ,Cl.Name AS ClientName
		  ,S.[Name]
		  ,S.[AddressLine1]
		  ,S.[AddressLine2]
		  ,S.[City]
		  ,S.[State]
		  ,S.[Zip]
		  ,S.[Zip4]
		  ,S.[WindowStartTime]
		  ,S.[WindowEndTime]
		  ,S.[ContactName]
		  ,S.[ContactPhone]
		  ,S.[ContactEmail]
		  ,S.[Status]
		  ,S.[LastUpdated]
		  ,S.[UserID]
	  FROM [Tsort].[dbo].[ltlShipper] AS S
	  LEFT JOIN [Tsort].[dbo].[ltlClient] AS Cl ON S.ClientID = Cl.ID 
	  WHERE (@ClientID = 0 OR ClientID = @ClientID)
  END

GO