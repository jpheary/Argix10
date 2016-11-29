USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLShipperZipRead]    Script Date: 03/03/2014 11:18:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLShipperZipRead] 
	@Zip CHAR(5)
AS
BEGIN
	SET NOCOUNT ON;

	EXEC Tsort1.dbo.uspLTLShipperZipRead @Zip
END

GO
