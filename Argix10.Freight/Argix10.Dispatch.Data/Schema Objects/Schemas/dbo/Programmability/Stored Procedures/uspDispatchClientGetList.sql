USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchClientGetList]    Script Date: 03/03/2014 11:31:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspDispatchClientGetList] 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	NUMBER AS Number, DIVISION AS Division, RTRIM(NAME) AS Name, STATUS AS Status
	FROM	CLIENT
	WHERE	DIVISION = '01' AND STATUS = 'A'
	ORDER BY NAME
END


GO


