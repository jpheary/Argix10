USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchDriverGetList]    Script Date: 03/03/2014 11:32:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspDispatchDriverGetList]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	DriverID AS ID, RTrim([FirstName]) + ' ' + RTrim([LastName]) AS Description
	FROM	[TSORT].[dbo].[tsDriver]
	WHERE	TerminalID = '100000532000000053' AND IsActive ='1'
	ORDER BY FirstName
END


GO


