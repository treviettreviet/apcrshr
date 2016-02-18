USE [master]
GO
/****** Object:  Database [APCRSHR]    Script Date: 2/18/2016 9:57:44 AM ******/
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
/****** Object:  Table [dbo].[Admin]    Script Date: 2/18/2016 9:57:44 AM ******/
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
/****** Object:  Table [dbo].[Article]    Script Date: 2/18/2016 9:57:44 AM ******/
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
/****** Object:  Table [dbo].[ConferenceData]    Script Date: 2/18/2016 9:57:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ConferenceData](
	[ConferenceDataID] [varchar](50) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Contents] [nvarchar](max) NOT NULL,
	[ActionURL] [nvarchar](200) NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[Language] [varchar](50) NULL,
	[MenuID] [varchar](50) NULL,
	[DocURL] [nvarchar](200) NULL,
	[ImageURL] [nvarchar](200) NULL,
 CONSTRAINT [PK_ConferenceData] PRIMARY KEY CLUSTERED 
(
	[ConferenceDataID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DownloadCode]    Script Date: 2/18/2016 9:57:44 AM ******/
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
/****** Object:  Table [dbo].[ImportantDeadline]    Script Date: 2/18/2016 9:57:44 AM ******/
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
/****** Object:  Table [dbo].[MailingAddress]    Script Date: 2/18/2016 9:57:44 AM ******/
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
/****** Object:  Table [dbo].[Menu]    Script Date: 2/18/2016 9:57:44 AM ******/
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
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[News]    Script Date: 2/18/2016 9:57:44 AM ******/
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
/****** Object:  Table [dbo].[OfficeAddress]    Script Date: 2/18/2016 9:57:44 AM ******/
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
/****** Object:  Table [dbo].[Slider]    Script Date: 2/18/2016 9:57:44 AM ******/
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
/****** Object:  Table [dbo].[Subscriber]    Script Date: 2/18/2016 9:57:44 AM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 2/18/2016 9:57:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [varchar](50) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[PhoneCode] [varchar](15) NULL,
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
/****** Object:  Table [dbo].[UserSession]    Script Date: 2/18/2016 9:57:44 AM ******/
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
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Locked]  DEFAULT ((0)) FOR [Locked]
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
USE [master]
GO
ALTER DATABASE [APCRSHR] SET  READ_WRITE 
GO
