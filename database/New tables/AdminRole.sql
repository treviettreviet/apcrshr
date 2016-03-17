USE [APCRSHR]
GO

/****** Object:  Table [dbo].[AdminRole]    Script Date: 3/16/2016 8:15:50 AM ******/
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

