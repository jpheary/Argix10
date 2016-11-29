USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLConsigneeGetList]    Script Date: 03/03/2014 11:08:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLConsigneesGetList] 
	@ClientID int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [ID]
		  ,[ClientID]
		  ,[Name]
		  ,[Zip]
	  FROM [Tsort].[dbo].[ltlConsignee]
	  WHERE (@ClientID = 0 OR ClientID = @ClientID) AND [Status] = 'A'
  END

GO