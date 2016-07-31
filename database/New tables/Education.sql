USE [APCRSHR]
GO

/****** Object:  Table [dbo].[Education]    Script Date: 7/30/2016 9:13:32 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Education](
	[EducationID] [varchar](50) NOT NULL,
	[Degree] [nvarchar](100) NOT NULL,
	[Country] [varchar](50) NOT NULL,
	[EducationStart] [datetime] NOT NULL,
	[EducationEnd] [datetime] NULL,
	[EducationNow] [bit] NOT NULL,
	[MainCourseStudy] [nvarchar](200) NOT NULL,
	[TypeOfTraining] [varchar](100) NOT NULL,
	[YouthScholarshipID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Education] PRIMARY KEY CLUSTERED 
(
	[EducationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Education] ADD  CONSTRAINT [DF_Education_EducationNow]  DEFAULT ((0)) FOR [EducationNow]
GO

ALTER TABLE [dbo].[Education]  WITH CHECK ADD  CONSTRAINT [FK_Education_YouthScholarship] FOREIGN KEY([YouthScholarshipID])
REFERENCES [dbo].[YouthScholarship] ([YouthScholarshipID])
GO

ALTER TABLE [dbo].[Education] CHECK CONSTRAINT [FK_Education_YouthScholarship]
GO


