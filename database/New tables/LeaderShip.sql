USE [APCRSHR]
GO

/****** Object:  Table [dbo].[LeaderShip]    Script Date: 7/30/2016 9:14:18 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LeaderShip](
	[LeaderShipID] [varchar](50) NOT NULL,
	[Organization] [nvarchar](200) NOT NULL,
	[Position] [nvarchar](100) NOT NULL,
	[FromDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[LeaderNow] [bit] NOT NULL,
	[Duties] [nvarchar](300) NOT NULL,
	[YouthScholarshipID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_LeaderShip] PRIMARY KEY CLUSTERED 
(
	[LeaderShipID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[LeaderShip] ADD  CONSTRAINT [DF_LeaderShip_LeaderNow]  DEFAULT ((0)) FOR [LeaderNow]
GO

ALTER TABLE [dbo].[LeaderShip]  WITH CHECK ADD  CONSTRAINT [FK_LeaderShip_YouthScholarship] FOREIGN KEY([YouthScholarshipID])
REFERENCES [dbo].[YouthScholarship] ([YouthScholarshipID])
GO

ALTER TABLE [dbo].[LeaderShip] CHECK CONSTRAINT [FK_LeaderShip_YouthScholarship]
GO


