USE [APCRSHR]
GO

/****** Object:  Table [dbo].[Experience]    Script Date: 7/30/2016 9:12:54 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Experience](
	[WorkingID] [varchar](50) NOT NULL,
	[Organization] [nvarchar](200) NOT NULL,
	[Position] [nvarchar](100) NOT NULL,
	[WorkingStart] [datetime] NOT NULL,
	[WorkingEnd] [datetime] NULL,
	[WorkingNow] [bit] NOT NULL,
	[Duties] [nvarchar](300) NULL,
	[YouthScholarshipID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Experience] PRIMARY KEY CLUSTERED 
(
	[WorkingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Experience] ADD  CONSTRAINT [DF_Table_1_StillWorking]  DEFAULT ((0)) FOR [WorkingNow]
GO

ALTER TABLE [dbo].[Experience]  WITH CHECK ADD  CONSTRAINT [FK_Experience_YouthScholarship] FOREIGN KEY([YouthScholarshipID])
REFERENCES [dbo].[YouthScholarship] ([YouthScholarshipID])
GO

ALTER TABLE [dbo].[Experience] CHECK CONSTRAINT [FK_Experience_YouthScholarship]
GO


