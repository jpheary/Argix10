USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLShipperGetList]    Script Date: 03/03/2014 11:18:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLShipperGetList] 
	@ClientID int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [ID]
		  ,[ClientID]
		  ,[Name]
		  ,[Zip]
	  FROM [Tsort].[dbo].[ltlShipper]
	  WHERE (@ClientID = 0 OR ClientID = @ClientID) AND [Status] = 'A'
  END

GO