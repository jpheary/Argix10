USE [Tsort]
GO

SET ANSI_NULLS ON
GO

CREATE TABLE [dbo].[ltlTariffRate](
	[OriginZone] [char](1) NOT NULL,
	[DestinationZipCode] [char](5) NOT NULL,
	[EffectiveDate] [datetime] NOT NULL,
	[Rate] [decimal](9, 2) NOT NULL,
 	[TransitMin] [int] NULL,
	[TransitMax] [int] NULL,
CONSTRAINT [PK_ltlTariffRate] PRIMARY KEY CLUSTERED 
(
	[OriginZone] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


