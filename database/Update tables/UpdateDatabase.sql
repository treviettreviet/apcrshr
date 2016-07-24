USE [APCRSHR]

GO

ALTER TABLE [Upload] ADD [FilePath] NVARCHAR(200) NULL

GO

DROP TABLE [CONFERENCEDATA]

GO
ALTER TABLE [ConferenceDeclaration] ADD [ActionURL] NVARCHAR(200) NOT NULL

GO

ALTER TABLE [Menu] ADD [Type] BIT NOT NULL DEFAULT(0)

GO

ALTER TABLE [Menu] ADD [URL] NVARCHAR(200) NULL

GO

ALTER TABLE [Menu] ADD [DisplayOrder] INT NOT NULL DEFAULT(0)

GO

ALTER TABLE [Presentation] ADD [ActionURL] NVARCHAR(200) NOT NULL

GO

ALTER TABLE [Photo] ADD [ActionURL] NVARCHAR(200) NOT NULL

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

ALTER TABLE [Admin] ADD [Type] INT NOT NULL DEFAULT(0)

GO

ALTER TABLE [Presentation]
ADD CONSTRAINT pk_PresentationID PRIMARY KEY (PresentationID)

GO

ALTER TABLE [AdminRole] 
DROP CONSTRAINT FK_AdminRole_Admin

GO

ALTER TABLE [AdminRole] 
DROP CONSTRAINT FK_AdminRole_Role

GO

ALTER TABLE [RoleResource] 
DROP CONSTRAINT FK_RoleResource_Resource

GO

ALTER TABLE [RoleResource] 
DROP CONSTRAINT FK_RoleResource_Role

GO

ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK_Photo_Album] FOREIGN KEY([AlbumID])
REFERENCES [dbo].[Album] ([AlbumID])

GO

ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK_Photo_Album]

GO

ALTER TABLE [Article] ALTER COLUMN [MenuID] VARCHAR(50) NULL

GO

ALTER TABLE [Article] ADD [ImageURL] NVARCHAR(200) NULL

GO

ALTER TABLE [Article] ADD [HomeDisplay] BIT NOT NULL DEFAULT(0)

GO

ALTER TABLE [User] ADD [MealPreference] VARCHAR(50) NULL

GO

ALTER TABLE [User] ADD [DisabilitySpecialTreatment] NVARCHAR(500) NULL

GO

ALTER TABLE [User] ALTER COLUMN [UserName] VARCHAR(100) NOT NULL

GO

DROP TABLE [OfficeAddress]

GO

ALTER TABLE [User] ADD [Address] NVARCHAR(200) NULL

GO

ALTER TABLE [User] ADD [City] VARCHAR(50) NULL

GO

ALTER TABLE [User] ADD [Country] VARCHAR(50) NULL

GO

ALTER TABLE [User] ADD [WorkAddress] NVARCHAR(200) NULL

GO

ALTER TABLE [User] ADD [Organization] NVARCHAR(200) NULL

GO

ALTER TABLE [User] ADD [RegistrationStatus] INT NOT NULL DEFAULT(0)

GO

ALTER TABLE [User] Alter COLUMN [DateOfBirth] DATETIME NULL

GO

ALTER TABLE [User] ADD [EmailDuplicationCode] VARCHAR(100) NULL

GO

ALTER TABLE [MailingAddress] DROP COLUMN [AddressLine1]

GO

ALTER TABLE [MailingAddress] DROP COLUMN [City]

GO

ALTER TABLE [MailingAddress] DROP COLUMN [StateProvince]

GO

ALTER TABLE [MailingAddress] DROP COLUMN [Country]

GO

ALTER TABLE [MailingAddress] DROP COLUMN [PostalCode]

GO

ALTER TABLE [MailingAddress] ADD [ParticipantType] VARCHAR(100) NOT NULL

GO

ALTER TABLE [MailingAddress] ADD [ParticipateYouth] VARCHAR(50) NOT NULL

GO

ALTER TABLE [MailingAddress] ADD [DateOfBirth] DATETIME NOT NULL

GO

ALTER TABLE [MailingAddress] ADD [OriginalNationality] VARCHAR(200) NOT NULL

GO

ALTER TABLE [MailingAddress] ADD [CurrentNationality] VARCHAR(200) NOT NULL

GO

ALTER TABLE [MailingAddress] ADD [PassportNumber] VARCHAR(50) NOT NULL

GO

ALTER TABLE [MailingAddress] ADD [TypeOfPassport] VARCHAR(50) NOT NULL

GO

ALTER TABLE [MailingAddress] ADD [Occupation] VARCHAR(200) NOT NULL

GO

ALTER TABLE [MailingAddress] ADD [DateOfPassportIssue] DATETIME NOT NULL

GO

ALTER TABLE [MailingAddress] ADD [DateOfPassportExpiry] DATETIME NOT NULL

GO

ALTER TABLE [MailingAddress] ADD [PassportPhoto1] NVARCHAR(200) NOT NULL

GO

ALTER TABLE [MailingAddress] ADD [PassportPhoto2] NVARCHAR(200) NOT NULL

GO

ALTER TABLE [MailingAddress] ADD [PassportPhoto3] NVARCHAR(200) NOT NULL

GO

ALTER TABLE [MailingAddress] ADD [DetailOfEmbassy] NVARCHAR(MAX) NULL

GO

ALTER TABLE [MailingAddress] ADD [NeedVisaSupport] BIT NOT NULL DEFAULT(0)

GO

ALTER TABLE [MailingAddress] ALTER COLUMN [PassportPhoto1] NVARCHAR(200) NULL

GO

ALTER TABLE [MailingAddress] ALTER COLUMN [PassportPhoto2] NVARCHAR(200) NULL

GO

ALTER TABLE [MailingAddress] ALTER COLUMN [PassportPhoto3] NVARCHAR(200) NULL

GO

ALTER TABLE [MailingAddress] ADD [ActivationCode] VARCHAR(50) NULL

GO

ALTER TABLE [MailingAddress] ADD [RegistrationNumber] VARCHAR(50) NULL

GO

ALTER TABLE [Session] ADD [Step] INT NOT NULL DEFAULT(0)

GO

ALTER TABLE [Session] ADD [Completed] BIT NOT NULL DEFAULT(0)

GO