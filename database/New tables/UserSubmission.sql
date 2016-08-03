USE [APCRSHR]
GO

/****** Object:  Table [dbo].[UserSubmission]    Script Date: 8/3/2016 11:59:15 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[UserSubmission](
	[SubmitID] [varchar](50) NOT NULL,
	[UserID] [varchar](50) NOT NULL,
	[SubmissionNumber] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UserSubmission] PRIMARY KEY CLUSTERED 
(
	[SubmitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[UserSubmission]  WITH CHECK ADD  CONSTRAINT [FK_UserSubmission_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO

ALTER TABLE [dbo].[UserSubmission] CHECK CONSTRAINT [FK_UserSubmission_User]
GO


