USE [APCRSHR]

GO

ALTER TABLE [ImportantDeadline] ADD [Type] INT NOT NULL DEFAULT(0)

GO

ALTER TABLE [MainScholarship] ADD [Status] INT NOT NULL DEFAULT(0)

GO

ALTER TABLE [UserSubmission] ADD [Status] INT NOT NULL DEFAULT(0)

GO

ALTER TABLE [Payment] DROP DF_Payment_Status

GO

ALTER TABLE [Payment] ALTER COLUMN [Status] INT NOT NULL

GO

ALTER TABLE [MailingAddress] ALTER COLUMN [PassportNumber] VARCHAR(50) NULL

GO

ALTER TABLE [MailingAddress] ALTER COLUMN [TypeOfPassport] VARCHAR(50) NULL

GO

ALTER TABLE [MailingAddress] ALTER COLUMN [DateOfPassportIssue] DATETIME NULL

GO

ALTER TABLE [MailingAddress] ALTER COLUMN [DateOfPassportExpiry] DATETIME NULL

GO

ALTER TABLE [MainScholarship] ADD [WorkingNow] BIT NULL DEFAULT (0)

GO

ALTER TABLE [MainScholarship] ADD [ScholarshipPackage] NVARCHAR(200) NULL

GO

ALTER TABLE [Payment] ADD [MerchRef] NVARCHAR(200) NULL

GO

ALTER TABLE [MainScholarship] ALTER COLUMN [Responsibility] NVARCHAR(1000) NOT NULL

GO

ALTER TABLE [MainScholarship] ALTER COLUMN [ReasonScholarship] NVARCHAR(1000) NOT NULL

GO