USE [APCRSHR]
GO

/****** Object:  Table [dbo].[Publication]    Script Date: 7/30/2016 9:15:05 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Publication](
	[PublicationID] [varchar](50) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Source] [nvarchar](100) NOT NULL,
	[Year] [int] NOT NULL,
	[URL] [nvarchar](200) NULL,
	[YouthScholarshipID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Publication] PRIMARY KEY CLUSTERED 
(
	[PublicationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Publication]  WITH CHECK ADD  CONSTRAINT [FK_Publication_YouthScholarship] FOREIGN KEY([YouthScholarshipID])
REFERENCES [dbo].[YouthScholarship] ([YouthScholarshipID])
GO

ALTER TABLE [dbo].[Publication] CHECK CONSTRAINT [FK_Publication_YouthScholarship]
GO


