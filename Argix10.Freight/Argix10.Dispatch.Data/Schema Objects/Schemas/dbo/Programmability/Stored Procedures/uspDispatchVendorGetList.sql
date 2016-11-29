USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchVendorGetList]    Script Date: 03/03/2014 11:33:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspDispatchVendorGetList] 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	NUMBER AS Number, NAME AS Name, STATUS AS Status, ADDRESS_LINE1 AS AddressLine1, ADDRESS_LINE2 AS AddressLine2, 
			CITY AS City, STATE AS State, ZIP AS Zip, ZIP4 AS Zip4
	FROM	VENDOR
	WHERE	STATUS = 'A'
	ORDER BY NAME
END


GO


