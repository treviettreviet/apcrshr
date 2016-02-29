USE [APCRSHR]
GO

/****** Object:  Table [dbo].[ConferenceDeclaration]    Script Date: 2/29/2016 9:47:55 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ConferenceDeclaration](
	[ConferenceID] [varchar](50) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[AttachmentURL] [nvarchar](200) NULL,
	[ImageURL] [nvarchar](200) NULL,
	[Contents] [nvarchar](max) NULL,
	[ShortContent] [nvarchar](300) NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_ConferenceDeclaration] PRIMARY KEY CLUSTERED 
(
	[ConferenceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

