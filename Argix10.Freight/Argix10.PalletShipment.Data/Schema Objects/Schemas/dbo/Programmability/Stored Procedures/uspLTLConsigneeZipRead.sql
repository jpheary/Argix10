USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspLTLConsigneeZipRead]    Script Date: 03/03/2014 11:09:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspLTLConsigneeZipRead] 
	@Zip CHAR(5)
AS
BEGIN
	SET NOCOUNT ON;

	EXEC Tsort1.dbo.uspLTLConsigneeZipRead @Zip
END

GO
