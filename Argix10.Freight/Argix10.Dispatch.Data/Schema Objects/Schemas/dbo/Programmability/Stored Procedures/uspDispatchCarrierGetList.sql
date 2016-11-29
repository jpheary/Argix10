USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchCarrierGetList]    Script Date: 03/03/2014 11:31:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspDispatchCarrierGetList] 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  CarrierServiceID AS [ID], RTRIM(CCS.CarrierName) AS [Description]
	FROM dbo.tsCarrierCarrierService AS CCS 
	INNER JOIN dbo.tsCarrierService AS CS ON CCS.ServiceID=CS.ServiceID 
	INNER JOIN dbo.tsCompany ON CCS.CarrierID=tsCompany.CompanyID
	WHERE CCS.IsActive=1
	ORDER BY CCS.CarrierName

	--SELECT	CompanyID AS ID, CompanyName AS DESCRIPTION
	--FROM	[TSORT].[dbo].[tsCompany]
	--WHERE	CompanyType = 40 AND IsActive = 1
	--ORDER BY CompanyName
END


GO


