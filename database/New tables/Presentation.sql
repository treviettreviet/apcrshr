USE [APCRSHR]
GO

/****** Object:  Table [dbo].[Presentation]    Script Date: 2/29/2016 9:48:24 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Presentation](
	[PresentationID] [varchar](50) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[AttachmentURL] [nvarchar](200) NULL,
	[ImageURL] [nvarchar](200) NULL,
	[Contents] [nvarchar](max) NULL,
	[ShortContent] [nvarchar](300) NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

