USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchLocationGetList]    Script Date: 03/03/2014 11:32:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspDispatchLocationGetList] 
AS
BEGIN
	SET NOCOUNT ON;

SELECT	L.LocationID AS ID
		,RTRIM(L.Description) AS Name
		,RTRIM(A.City) + ', ' + RTRIM(A.StateOrProvince) AS Location
  FROM TSORT.dbo.tsLocation AS L
  INNER JOIN TSORT.dbo.tsLocationType AS LT ON LT.LocationTypeID = L.LocationTypeID
  INNER JOIN TSORT.dbo.tsAddress AS A ON A.LocationID = L.LocationID
  WHERE L.IsActive = 1 AND (L.LocationTypeID = 20 OR L.LocationTypeID = 30) AND 
		(A.StateOrProvince = 'ME' OR A.StateOrProvince = 'NH' OR A.StateOrProvince = 'VT' OR 
		 A.StateOrProvince = 'CT' OR A.StateOrProvince = 'MA' OR A.StateOrProvince = 'RI' OR 
		 A.StateOrProvince = 'NY' OR A.StateOrProvince = 'NJ' OR A.StateOrProvince = 'PA' OR 
		 A.StateOrProvince = 'DE' OR A.StateOrProvince = 'MD' OR A.StateOrProvince = 'VA' OR 
		 A.StateOrProvince = 'WV' OR A.StateOrProvince = 'NC' OR A.StateOrProvince = 'SC' OR 
		 A.StateOrProvince = 'GA' OR A.StateOrProvince = 'KY' OR A.StateOrProvince = 'TN')
  ORDER BY L.Description
END


GO


