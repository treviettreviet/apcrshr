USE [master]
GO
/****** Object:  Database [APCRSHR]    Script Date: 3/16/2016 8:16:21 AM ******/
CREATE DATABASE [APCRSHR]
GO
ALTER DATABASE [APCRSHR] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [APCRSHR] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [APCRSHR] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [APCRSHR] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [APCRSHR] SET ARITHABORT OFF 
GO
ALTER DATABASE [APCRSHR] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [APCRSHR] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [APCRSHR] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [APCRSHR] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [APCRSHR] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [APCRSHR] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [APCRSHR] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [APCRSHR] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [APCRSHR] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [APCRSHR] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [APCRSHR] SET  DISABLE_BROKER 
GO
ALTER DATABASE [APCRSHR] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [APCRSHR] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [APCRSHR] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [APCRSHR] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [APCRSHR] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [APCRSHR] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [APCRSHR] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [APCRSHR] SET RECOVERY FULL 
GO
ALTER DATABASE [APCRSHR] SET  MULTI_USER 
GO
ALTER DATABASE [APCRSHR] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [APCRSHR] SET DB_CHAINING OFF 
GO
ALTER DATABASE [APCRSHR] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [APCRSHR] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'APCRSHR', N'ON'
GO
USE [APCRSHR]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 3/16/2016 8:16:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Admin](
	[AdminID] [varchar](50) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[BirthDate] [datetime] NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[Locked] [bit] NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[AdminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AdminRole]    Script Date: 3/16/2016 8:16:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdminRole](
	[AdminID] [varchar](50) NOT NULL,
	[RoleID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_AdminRole] PRIMARY KEY CLUSTERED 
(
	[AdminID] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Album]    Script Date: 3/16/2016 8:16:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Album](
	[AlbumID] [varchar](50) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[ActionURL] [nvarchar](200) NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[Description] [nvarchar](300) NULL,
 CONSTRAINT [PK_Album] PRIMARY KEY CLUSTERED 
(
	[AlbumID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Article]    Script Date: 3/16/2016 8:16:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Article](
	[ArticleID] [varchar](50) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Contents] [nvarchar](max) NOT NULL,
	[ActionURL] [nvarchar](200) NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[Language] [varchar](50) NULL,
	[MenuID] [varchar](50) NOT NULL,
	[ShortContent] [nvarchar](300) NULL,
 CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED 
(
	[ArticleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ConferenceDeclaration]    Script Date: 3/16/2016 8:16:21 AM ******/
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
	[ActionURL] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_ConferenceDeclaration] PRIMARY KEY CLUSTERED 
(
	[ConferenceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DownloadCode]    Script Date: 3/16/2016 8:16:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DownloadCode](
	[DownloadID] [varchar](50) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ValidDate] [datetime] NOT NULL,
	[UserID] [varchar](50) NOT NULL,
	[PaymentID] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_DownloadCode] PRIMARY KEY CLUSTERED 
(
	[DownloadID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ImportantDeadline]    Script Date: 3/16/2016 8:16:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ImportantDeadline](
	[DeadlineID] [varchar](50) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[ShortContent] [nvarchar](300) NULL,
	[Contents] [nvarchar](max) NULL,
	[ActionURL] [nvarchar](200) NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[Deadline] [datetime] NOT NULL,
 CONSTRAINT [PK_ImportantDeadline] PRIMARY KEY CLUSTERED 
(
	[DeadlineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MailingAddress]    Script Date: 3/16/2016 8:16:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MailingAddress](
	[MailingAddressID] [varchar](50) NOT NULL,
	[AddressLine1] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[StateProvince] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[PostalCode] [varchar](50) NULL,
	[UserID] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_MailingAddress] PRIMARY KEY CLUSTERED 
(
	[MailingAddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 3/16/2016 8:16:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Menu](
	[MenuID] [varchar](50) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[ActionURL] [nvarchar](200) NOT NULL,
	[Language] [varchar](50) NULL,
	[ParentID] [varchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[URL] [nvarchar](200) NULL,
	[Type] [bit] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[News]    Script Date: 3/16/2016 8:16:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[News](
	[NewsID] [varchar](50) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Contents] [nvarchar](max) NOT NULL,
	[ActionURL] [nvarchar](200) NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[Language] [varchar](50) NULL,
	[ShortContent] [nvarchar](300) NOT NULL,
	[ThumbnailURL] [nvarchar](200) NULL,
	[MenuID] [varchar](50) NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[NewsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OfficeAddress]    Script Date: 3/16/2016 8:16:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OfficeAddress](
	[OfficeAddressID] [varchar](50) NOT NULL,
	[Institute] [nvarchar](50) NULL,
	[Street] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[Department] [nvarchar](50) NULL,
	[StateProvince] [nvarchar](50) NULL,
	[PostalCode] [varchar](50) NULL,
	[UserID] [varchar](50) NOT NULL,
	[Phone] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_OfficeAddress] PRIMARY KEY CLUSTERED 
(
	[OfficeAddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Photo]    Script Date: 3/16/2016 8:16:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Photo](
	[PhotoID] [varchar](50) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[ImageURL] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](300) NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[AlbumID] [varchar](50) NOT NULL,
	[ActionURL] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Photo] PRIMARY KEY CLUSTERED 
(
	[PhotoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Presentation]    Script Date: 3/16/2016 8:16:21 AM ******/
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
	[UpdatedDate] [datetime] NULL,
	[ActionURL] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Presentation] PRIMARY KEY CLUSTERED 
(
	[PresentationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Resource]    Script Date: 3/16/2016 8:16:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Resource](
	[ResourceID] [varchar](50) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[URL] [nvarchar](200) NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED 
(
	[ResourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Role]    Script Date: 3/16/2016 8:16:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [varchar](50) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](300) NULL,
	[Type] [int] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoleResource]    Script Date: 3/16/2016 8:16:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RoleResource](
	[ResourceID] [varchar](50) NOT NULL,
	[RoleID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_RoleResource] PRIMARY KEY CLUSTERED 
(
	[ResourceID] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Slider]    Script Date: 3/16/2016 8:16:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Slider](
	[SliderID] [varchar](50) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[URL] [nvarchar](200) NULL,
	[ImageURL] [nvarchar](200) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[Language] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Slider] PRIMARY KEY CLUSTERED 
(
	[SliderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Subscriber]    Script Date: 3/16/2016 8:16:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Subscriber](
	[SubscriberID] [varchar](50) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Contents] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Phone] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Address] [nvarchar](200) NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[SubscriberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Upload]    Script Date: 3/16/2016 8:16:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Upload](
	[UploadID] [varchar](50) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[UploadURL] [nvarchar](200) NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[FilePath] [nvarchar](200) NULL,
 CONSTRAINT [PK_Upload] PRIMARY KEY CLUSTERED 
(
	[UploadID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 3/16/2016 8:16:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [varchar](50) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[Sex] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[OtherEmail] [varchar](100) NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[PhoneNumber] [varchar](50) NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[Locked] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserSession]    Script Date: 3/16/2016 8:16:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserSession](
	[SessionID] [varchar](50) NOT NULL,
	[UserID] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_UserSession] PRIMARY KEY CLUSTERED 
(
	[SessionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Video]    Script Date: 3/16/2016 8:16:21 AM ******/
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
ALTER TABLE [dbo].[Menu] ADD  DEFAULT ((0)) FOR [Type]
GO
ALTER TABLE [dbo].[Menu] ADD  DEFAULT ((0)) FOR [DisplayOrder]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_Type]  DEFAULT ((0)) FOR [Type]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Locked]  DEFAULT ((0)) FOR [Locked]
GO
ALTER TABLE [dbo].[AdminRole]  WITH CHECK ADD  CONSTRAINT [FK_AdminRole_Admin] FOREIGN KEY([AdminID])
REFERENCES [dbo].[Admin] ([AdminID])
GO
ALTER TABLE [dbo].[AdminRole] CHECK CONSTRAINT [FK_AdminRole_Admin]
GO
ALTER TABLE [dbo].[AdminRole]  WITH CHECK ADD  CONSTRAINT [FK_AdminRole_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[AdminRole] CHECK CONSTRAINT [FK_AdminRole_Role]
GO
ALTER TABLE [dbo].[MailingAddress]  WITH CHECK ADD  CONSTRAINT [FK_MailingAddress_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[MailingAddress] CHECK CONSTRAINT [FK_MailingAddress_User]
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD  CONSTRAINT [FK_Menu_Menu] FOREIGN KEY([ParentID])
REFERENCES [dbo].[Menu] ([MenuID])
GO
ALTER TABLE [dbo].[Menu] CHECK CONSTRAINT [FK_Menu_Menu]
GO
ALTER TABLE [dbo].[OfficeAddress]  WITH CHECK ADD  CONSTRAINT [FK_OfficeAddress_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[OfficeAddress] CHECK CONSTRAINT [FK_OfficeAddress_User]
GO
ALTER TABLE [dbo].[RoleResource]  WITH CHECK ADD  CONSTRAINT [FK_RoleResource_Resource] FOREIGN KEY([ResourceID])
REFERENCES [dbo].[Resource] ([ResourceID])
GO
ALTER TABLE [dbo].[RoleResource] CHECK CONSTRAINT [FK_RoleResource_Resource]
GO
ALTER TABLE [dbo].[RoleResource]  WITH CHECK ADD  CONSTRAINT [FK_RoleResource_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[RoleResource] CHECK CONSTRAINT [FK_RoleResource_Role]
GO
USE [master]
GO
ALTER DATABASE [APCRSHR] SET  READ_WRITE 
GO
