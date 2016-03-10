USE [APCRSHR]

GO

--Drop columns

ALTER TABLE [User]
DROP COLUMN [FirstName]

GO

ALTER TABLE [User]
DROP COLUMN [LastName]

GO

ALTER TABLE [User]
DROP COLUMN [PhoneCode]

GO

--Add columns

ALTER TABLE [User]
ADD [FullName] NVARCHAR(100) NOT NULL

GO

ALTER TABLE [User]
ADD [Sex] VARCHAR(50) NOT NULL

GO

ALTER TABLE [User]
ADD [OtherEmail] VARCHAR(100) NULL

GO

ALTER TABLE [User]
ADD [DateOfBirth] DATETIME NOT NULL