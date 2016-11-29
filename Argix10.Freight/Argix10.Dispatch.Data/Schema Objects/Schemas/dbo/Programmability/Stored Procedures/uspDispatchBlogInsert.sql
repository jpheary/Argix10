USE [Tsort]
GO

/****** Object:  StoredProcedure [dbo].[uspDispatchBlogInsert]    Script Date: 03/03/2014 11:31:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[uspDispatchBlogInsert] 
	@Date datetime,
	@Comment varchar(300),
	@UserID varchar(50)
AS
BEGIN

INSERT INTO Tsort.dbo.dspBlog 
			( Date, Comment, UserID )
     VALUES ( @Date, @Comment, @UserID )
END



GO


