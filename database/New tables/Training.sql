USE [APCRSHR]
GO

/****** Object:  Table [dbo].[Training]    Script Date: 7/30/2016 9:15:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Training](
	[TrainingID] [varchar](50) NOT NULL,
	[NameOfCourse] [nvarchar](100) NOT NULL,
	[Country] [varchar](50) NOT NULL,
	[TrainingStart] [datetime] NOT NULL,
	[TrainingEnd] [datetime] NULL,
	[TrainingNow] [bit] NOT NULL,
	[YouthScholarshipID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Training] PRIMARY KEY CLUSTERED 
(
	[TrainingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Training] ADD  CONSTRAINT [DF_Training_TrainingNow]  DEFAULT ((0)) FOR [TrainingNow]
GO

ALTER TABLE [dbo].[Training]  WITH CHECK ADD  CONSTRAINT [FK_Training_YouthScholarship] FOREIGN KEY([YouthScholarshipID])
REFERENCES [dbo].[YouthScholarship] ([YouthScholarshipID])
GO

ALTER TABLE [dbo].[Training] CHECK CONSTRAINT [FK_Training_YouthScholarship]
GO


