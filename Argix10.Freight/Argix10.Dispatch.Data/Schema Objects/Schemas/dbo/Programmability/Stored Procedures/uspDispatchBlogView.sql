USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchBlogView]    Script Date: 03/03/2014 11:31:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[uspDispatchBlogView]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	[Date], Comment, UserID
	FROM	dspBlog
	WHERE	[Date] >= dateadd(d,-1,getdate()) AND [Date] <= getdate()
	ORDER BY [Date] ASC
END




GO


