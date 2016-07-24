USE [master]
GO
/****** Object:  Database [APCRSHR]    Script Date: 7/24/2016 4:25:22 AM ******/
CREATE DATABASE [APCRSHR]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'APCRSHR', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\APCRSHR.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'APCRSHR_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\APCRSHR_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [APCRSHR] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [APCRSHR].[dbo].[sp_fulltext_database] @action = 'enable'
end
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
ALTER DATABASE [APCRSHR] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'APCRSHR', N'ON'
GO
USE [APCRSHR]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 7/24/2016 4:25:22 AM ******/
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
	[Type] [int] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[AdminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AdminRole]    Script Date: 7/24/2016 4:25:22 AM ******/
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
/****** Object:  Table [dbo].[Album]    Script Date: 7/24/2016 4:25:22 AM ******/
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
/****** Object:  Table [dbo].[Article]    Script Date: 7/24/2016 4:25:22 AM ******/
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
	[MenuID] [varchar](50) NULL,
	[ShortContent] [nvarchar](300) NULL,
	[ImageURL] [nvarchar](200) NULL,
	[HomeDisplay] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED 
(
	[ArticleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ConferenceDeclaration]    Script Date: 7/24/2016 4:25:22 AM ******/
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
/****** Object:  Table [dbo].[DownloadCode]    Script Date: 7/24/2016 4:25:22 AM ******/
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
/****** Object:  Table [dbo].[ImportantDeadline]    Script Date: 7/24/2016 4:25:22 AM ******/
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
/****** Object:  Table [dbo].[MailingAddress]    Script Date: 7/24/2016 4:25:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MailingAddress](
	[MailingAddressID] [varchar](50) NOT NULL,
	[UserID] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[ParticipantType] [varchar](100) NOT NULL,
	[ParticipateYouth] [varchar](50) NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[OriginalNationality] [varchar](200) NOT NULL,
	[CurrentNationality] [varchar](200) NOT NULL,
	[PassportNumber] [varchar](50) NOT NULL,
	[TypeOfPassport] [varchar](50) NOT NULL,
	[Occupation] [varchar](200) NOT NULL,
	[DateOfPassportIssue] [datetime] NOT NULL,
	[DateOfPassportExpiry] [datetime] NOT NULL,
	[PassportPhoto1] [nvarchar](200) NULL,
	[PassportPhoto2] [nvarchar](200) NULL,
	[PassportPhoto3] [nvarchar](200) NULL,
	[DetailOfEmbassy] [nvarchar](max) NULL,
	[NeedVisaSupport] [bit] NOT NULL DEFAULT ((0)),
	[ActivationCode] [varchar](50) NULL,
	[RegistrationNumber] [varchar](50) NULL,
 CONSTRAINT [PK_MailingAddress] PRIMARY KEY CLUSTERED 
(
	[MailingAddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 7/24/2016 4:25:22 AM ******/
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
	[Type] [bit] NOT NULL DEFAULT ((0)),
	[DisplayOrder] [int] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[News]    Script Date: 7/24/2016 4:25:22 AM ******/
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
/****** Object:  Table [dbo].[Photo]    Script Date: 7/24/2016 4:25:22 AM ******/
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
/****** Object:  Table [dbo].[Presentation]    Script Date: 7/24/2016 4:25:22 AM ******/
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
/****** Object:  Table [dbo].[Resource]    Script Date: 7/24/2016 4:25:22 AM ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 7/24/2016 4:25:22 AM ******/
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
	[Type] [int] NOT NULL CONSTRAINT [DF_Role_Type]  DEFAULT ((0)),
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
/****** Object:  Table [dbo].[RoleResource]    Script Date: 7/24/2016 4:25:22 AM ******/
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
/****** Object:  Table [dbo].[Session]    Script Date: 7/24/2016 4:25:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Session](
	[SessionID] [varchar](50) NOT NULL,
	[Options] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[Step] [int] NOT NULL,
	[Completed] [bit] NOT NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[SessionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Slider]    Script Date: 7/24/2016 4:25:22 AM ******/
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
/****** Object:  Table [dbo].[Subscriber]    Script Date: 7/24/2016 4:25:22 AM ******/
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
/****** Object:  Table [dbo].[Upload]    Script Date: 7/24/2016 4:25:22 AM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 7/24/2016 4:25:22 AM ******/
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
	[DateOfBirth] [datetime] NULL,
	[PhoneNumber] [varchar](50) NULL,
	[UserName] [varchar](100) NOT NULL,
	[Password] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[Locked] [bit] NOT NULL CONSTRAINT [DF_User_Locked]  DEFAULT ((0)),
	[MealPreference] [varchar](50) NULL,
	[DisabilitySpecialTreatment] [nvarchar](500) NULL,
	[Address] [nvarchar](200) NULL,
	[City] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
	[WorkAddress] [nvarchar](200) NULL,
	[Organization] [nvarchar](200) NULL,
	[RegistrationStatus] [int] NOT NULL DEFAULT ((0)),
	[EmailDuplicationCode] [varchar](100) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserSession]    Script Date: 7/24/2016 4:25:22 AM ******/
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
/****** Object:  Table [dbo].[Video]    Script Date: 7/24/2016 4:25:22 AM ******/
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
ALTER TABLE [dbo].[Session] ADD  DEFAULT ((0)) FOR [Step]
GO
ALTER TABLE [dbo].[Session] ADD  DEFAULT ((0)) FOR [Completed]
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
ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK_Photo_Album] FOREIGN KEY([AlbumID])
REFERENCES [dbo].[Album] ([AlbumID])
GO
ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK_Photo_Album]
GO
USE [master]
GO
ALTER DATABASE [APCRSHR] SET  READ_WRITE 
GO
