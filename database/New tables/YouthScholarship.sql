USE [APCRSHR]
GO

/****** Object:  Table [dbo].[YouthScholarship]    Script Date: 8/4/2016 2:50:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[YouthScholarship](
	[YouthScholarshipID] [varchar](50) NOT NULL,
	[MotivationIssue] [nvarchar](1000) NOT NULL,
	[MotivationResolving] [nvarchar](1000) NULL,
	[MotivationReason] [nvarchar](1000) NOT NULL,
	[PlanMaking] [nvarchar](1000) NOT NULL,
	[UploadFile] [nvarchar](200) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UserID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_YouthScholarship] PRIMARY KEY CLUSTERED 
(
	[YouthScholarshipID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[YouthScholarship]  WITH CHECK ADD  CONSTRAINT [FK_YouthScholarship_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO

ALTER TABLE [dbo].[YouthScholarship] CHECK CONSTRAINT [FK_YouthScholarship_User]
GO


