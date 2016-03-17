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

ALTER TABLE [Photo] 
ADD FOREIGN KEY (AlbumID) REFERENCES Album(AlbumID)
GO
