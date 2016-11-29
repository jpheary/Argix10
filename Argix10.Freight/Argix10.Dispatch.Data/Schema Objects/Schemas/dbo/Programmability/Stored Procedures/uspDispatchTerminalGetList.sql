USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchTerminalGetList]    Script Date: 03/03/2014 11:33:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspDispatchTerminalGetList] 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	L.LocationID AS ID, L.Description AS Name
	FROM	TSORT.dbo.tsLocation AS L
	INNER JOIN TSORT.dbo.tsLocationType AS LT ON LT.LocationTypeID = L.LocationTypeID
	WHERE L.IsActive = 1 AND (L.LocationTypeID = 20)
	ORDER BY L.Description
END


GO


