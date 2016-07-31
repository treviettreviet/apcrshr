USE [APCRSHR]
GO

/****** Object:  Table [dbo].[MainScholarship]    Script Date: 7/30/2016 9:11:32 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MainScholarship](
	[ScholarshipID] [varchar](50) NOT NULL,
	[RegistrationNumber] [nvarchar](50) NOT NULL,
	[SubmissionNumber] [varchar](50) NOT NULL,
	[Responsibility] [nvarchar](200) NOT NULL,
	[ReasonScholarship] [nvarchar](500) NOT NULL,
	[HasSubmitted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_MainScholarship] PRIMARY KEY CLUSTERED 
(
	[ScholarshipID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[MainScholarship] ADD  CONSTRAINT [DF_MainScholarship_HasSubmitted]  DEFAULT ((0)) FOR [HasSubmitted]
GO


