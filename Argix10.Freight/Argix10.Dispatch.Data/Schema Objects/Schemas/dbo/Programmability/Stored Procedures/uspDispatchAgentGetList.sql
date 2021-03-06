USE [Tsort]
GO
/****** Object:  StoredProcedure [dbo].[uspDispatchAgentGetList]    Script Date: 03/03/2014 11:30:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[uspDispatchAgentGetList] 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	NUMBER AS Number, RTRIM(NAME) AS Name, STATUS AS Status, RTRIM(ADDRESS_LINE1) AS AddressLine1, RTRIM(ADDRESS_LINE2) AS AddressLine2, 
			RTRIM(CITY) AS City, STATE AS State, ZIP AS Zip, ZIP4 AS Zip4, ContactName, Phone, ParentNumber, ScannerType, ScanGunQty, AgentType
	FROM	AGENT
	WHERE	STATUS = 'A'
	ORDER BY NAME
END

