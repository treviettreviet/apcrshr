USE [APCRSHR]
GO

/****** Object:  Table [dbo].[Video]    Script Date: 3/7/2016 7:09:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Video](
	[VideoID] [nvarchar](50) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Contents] [nvarchar](max) NULL,
	[ActionURL] [nvarchar](200) NOT NULL,
	[VideoURL] [nvarchar](200) NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[Language] [varchar](50) NULL,
	[ShortContent] [nvarchar](300) NULL,
 CONSTRAINT [PK_Video] PRIMARY KEY CLUSTERED 
(
	[VideoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

