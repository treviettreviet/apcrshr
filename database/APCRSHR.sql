USE [master]
GO
/****** Object:  Database [APCRSHR]    Script Date: 9/1/2016 8:15:24 AM ******/
CREATE DATABASE [APCRSHR]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'APCRSHR', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\APCRSHR.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
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
/****** Object:  Table [dbo].[Admin]    Script Date: 9/1/2016 8:15:25 AM ******/
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
/****** Object:  Table [dbo].[AdminRole]    Script Date: 9/1/2016 8:15:25 AM ******/
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
/****** Object:  Table [dbo].[Album]    Script Date: 9/1/2016 8:15:25 AM ******/
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
/****** Object:  Table [dbo].[Article]    Script Date: 9/1/2016 8:15:25 AM ******/
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
/****** Object:  Table [dbo].[ConferenceDeclaration]    Script Date: 9/1/2016 8:15:25 AM ******/
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
/****** Object:  Table [dbo].[DownloadCode]    Script Date: 9/1/2016 8:15:25 AM ******/
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
/****** Object:  Table [dbo].[Education]    Script Date: 9/1/2016 8:15:25 AM ******/
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
/****** Object:  Table [dbo].[Experience]    Script Date: 9/1/2016 8:15:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Experience](
	[WorkingID] [varchar](50) NOT NULL,
	[Organization] [nvarchar](200) NOT NULL,
	[Position] [nvarchar](100) NOT NULL,
	[WorkingStart] [datetime] NOT NULL,
	[WorkingEnd] [datetime] NULL,
	[Duties] [nvarchar](300) NULL,
	[YouthScholarshipID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Experience] PRIMARY KEY CLUSTERED 
(
	[WorkingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ImportantDeadline]    Script Date: 9/1/2016 8:15:25 AM ******/
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
	[Type] [int] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_ImportantDeadline] PRIMARY KEY CLUSTERED 
(
	[DeadlineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LeaderShip]    Script Date: 9/1/2016 8:15:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LeaderShip](
	[LeaderShipID] [varchar](50) NOT NULL,
	[Organization] [nvarchar](200) NOT NULL,
	[Position] [nvarchar](100) NOT NULL,
	[FromDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[Duties] [nvarchar](300) NOT NULL,
	[YouthScholarshipID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_LeaderShip] PRIMARY KEY CLUSTERED 
(
	[LeaderShipID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MailingAddress]    Script Date: 9/1/2016 8:15:25 AM ******/
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
/****** Object:  Table [dbo].[MainScholarship]    Script Date: 9/1/2016 8:15:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MainScholarship](
	[ScholarshipID] [varchar](50) NOT NULL,
	[Responsibility] [nvarchar](200) NOT NULL,
	[ReasonScholarship] [nvarchar](500) NOT NULL,
	[HasSubmitted] [bit] NOT NULL CONSTRAINT [DF_MainScholarship_HasSubmitted]  DEFAULT ((0)),
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[DurationOfWork] [int] NOT NULL,
	[SubmissionTitles] [nvarchar](1000) NOT NULL,
	[WorkingNow] [bit] NOT NULL DEFAULT ((0)),
	[UserID] [varchar](50) NOT NULL,
	[Organization] [nvarchar](200) NOT NULL,
	[Position] [nvarchar](100) NOT NULL,
	[Status] [int] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_MainScholarship] PRIMARY KEY CLUSTERED 
(
	[ScholarshipID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 9/1/2016 8:15:25 AM ******/
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
/****** Object:  Table [dbo].[News]    Script Date: 9/1/2016 8:15:25 AM ******/
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
/****** Object:  Table [dbo].[Payment]    Script Date: 9/1/2016 8:15:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Payment](
	[PaymentID] [varchar](50) NOT NULL,
	[UserID] [varchar](50) NOT NULL,
	[PaymentType] [varchar](100) NOT NULL,
	[Amount] [float] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Photo]    Script Date: 9/1/2016 8:15:25 AM ******/
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
/****** Object:  Table [dbo].[Presentation]    Script Date: 9/1/2016 8:15:25 AM ******/
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
/****** Object:  Table [dbo].[Publication]    Script Date: 9/1/2016 8:15:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Publication](
	[PublicationID] [varchar](50) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Source] [nvarchar](100) NOT NULL,
	[Year] [int] NOT NULL,
	[URL] [nvarchar](200) NULL,
	[YouthScholarshipID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Publication] PRIMARY KEY CLUSTERED 
(
	[PublicationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Resource]    Script Date: 9/1/2016 8:15:25 AM ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 9/1/2016 8:15:25 AM ******/
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
/****** Object:  Table [dbo].[RoleResource]    Script Date: 9/1/2016 8:15:25 AM ******/
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
/****** Object:  Table [dbo].[Session]    Script Date: 9/1/2016 8:15:25 AM ******/
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
/****** Object:  Table [dbo].[Slider]    Script Date: 9/1/2016 8:15:25 AM ******/
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
/****** Object:  Table [dbo].[Subscriber]    Script Date: 9/1/2016 8:15:25 AM ******/
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
/****** Object:  Table [dbo].[Training]    Script Date: 9/1/2016 8:15:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Training](
	[TrainingID] [varchar](50) NOT NULL,
	[NameOfCourse] [nvarchar](100) NOT NULL,
	[Country] [varchar](50) NOT NULL,
	[TrainingStart] [datetime] NOT NULL,
	[TrainingEnd] [datetime] NULL,
	[YouthScholarshipID] [varchar](50) NOT NULL,
	[TypeOfTraining] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Training] PRIMARY KEY CLUSTERED 
(
	[TrainingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Upload]    Script Date: 9/1/2016 8:15:25 AM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 9/1/2016 8:15:25 AM ******/
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
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserSession]    Script Date: 9/1/2016 8:15:25 AM ******/
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
/****** Object:  Table [dbo].[UserSubmission]    Script Date: 9/1/2016 8:15:25 AM ******/
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
/****** Object:  Table [dbo].[Video]    Script Date: 9/1/2016 8:15:25 AM ******/
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
/****** Object:  Table [dbo].[YouthScholarship]    Script Date: 9/1/2016 8:15:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[YouthScholarship](
	[YouthScholarshipID] [varchar](50) NOT NULL,
	[MotivationIssue] [nvarchar](1000) NULL,
	[MotivationResolving] [nvarchar](1000) NULL,
	[MotivationReason] [nvarchar](1000) NULL,
	[PlanMaking] [nvarchar](1000) NULL,
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
ALTER TABLE [dbo].[Payment] ADD  CONSTRAINT [DF_Payment_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Session] ADD  DEFAULT ((0)) FOR [Step]
GO
ALTER TABLE [dbo].[Session] ADD  DEFAULT ((0)) FOR [Completed]
GO
ALTER TABLE [dbo].[Education]  WITH CHECK ADD  CONSTRAINT [FK_Education_YouthScholarship] FOREIGN KEY([YouthScholarshipID])
REFERENCES [dbo].[YouthScholarship] ([YouthScholarshipID])
GO
ALTER TABLE [dbo].[Education] CHECK CONSTRAINT [FK_Education_YouthScholarship]
GO
ALTER TABLE [dbo].[Experience]  WITH CHECK ADD  CONSTRAINT [FK_Experience_YouthScholarship] FOREIGN KEY([YouthScholarshipID])
REFERENCES [dbo].[YouthScholarship] ([YouthScholarshipID])
GO
ALTER TABLE [dbo].[Experience] CHECK CONSTRAINT [FK_Experience_YouthScholarship]
GO
ALTER TABLE [dbo].[LeaderShip]  WITH CHECK ADD  CONSTRAINT [FK_LeaderShip_YouthScholarship] FOREIGN KEY([YouthScholarshipID])
REFERENCES [dbo].[YouthScholarship] ([YouthScholarshipID])
GO
ALTER TABLE [dbo].[LeaderShip] CHECK CONSTRAINT [FK_LeaderShip_YouthScholarship]
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
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_User]
GO
ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK_Photo_Album] FOREIGN KEY([AlbumID])
REFERENCES [dbo].[Album] ([AlbumID])
GO
ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK_Photo_Album]
GO
ALTER TABLE [dbo].[Publication]  WITH CHECK ADD  CONSTRAINT [FK_Publication_YouthScholarship] FOREIGN KEY([YouthScholarshipID])
REFERENCES [dbo].[YouthScholarship] ([YouthScholarshipID])
GO
ALTER TABLE [dbo].[Publication] CHECK CONSTRAINT [FK_Publication_YouthScholarship]
GO
ALTER TABLE [dbo].[Training]  WITH CHECK ADD  CONSTRAINT [FK_Training_YouthScholarship] FOREIGN KEY([YouthScholarshipID])
REFERENCES [dbo].[YouthScholarship] ([YouthScholarshipID])
GO
ALTER TABLE [dbo].[Training] CHECK CONSTRAINT [FK_Training_YouthScholarship]
GO
ALTER TABLE [dbo].[UserSubmission]  WITH CHECK ADD  CONSTRAINT [FK_UserSubmission_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[UserSubmission] CHECK CONSTRAINT [FK_UserSubmission_User]
GO
USE [master]
GO
ALTER DATABASE [APCRSHR] SET  READ_WRITE 
GO
