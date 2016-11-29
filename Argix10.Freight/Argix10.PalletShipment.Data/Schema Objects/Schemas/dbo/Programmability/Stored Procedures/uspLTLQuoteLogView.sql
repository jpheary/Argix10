USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLQuoteLogView]    Script Date: 03/03/2014 11:13:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLQuoteLogView] 

AS
BEGIN
	SET NOCOUNT ON;

	SELECT	*
	FROM	[dbo].[ltlQuoteLog]
	ORDER BY Created DESC
END

GO