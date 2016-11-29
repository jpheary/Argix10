USE [Tsort]
GO

SET ANSI_NULLS ON
GO

CREATE TABLE [dbo].[ltlServiceLocation](
	[ZipCode] [char](5) NOT NULL,
	[City] [varchar](30) NULL,
	[State] [char](2) NULL,
	[Zone] [char](1) NOT NULL,
	[AgentNumber] [char](4) NULL,
 CONSTRAINT [PK_ltlServiceLocation] PRIMARY KEY CLUSTERED 
(
	[ZipCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

