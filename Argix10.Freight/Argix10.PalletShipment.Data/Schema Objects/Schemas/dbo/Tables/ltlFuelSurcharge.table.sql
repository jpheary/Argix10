USE [Tsort]
GO

SET ANSI_NULLS ON
GO

CREATE TABLE [dbo].[ltlFuelSurcharge](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Price] [decimal](9, 2) NOT NULL,
	[EffectiveDate] [datetime] NOT NULL,
	[Surcharge] [decimal](9, 2) NOT NULL,
 CONSTRAINT [PK_ltlFuelSurcharge] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO