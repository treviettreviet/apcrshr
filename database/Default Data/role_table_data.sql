USE [APCRSHR]

GO

INSERT [dbo].[Role] ([RoleID], [Name], [Description], [Type], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'0d9427d6-286c-4ea6-94d2-e56c7162a4f8', N'Resource Manager', N'/// Management items below: - Article, Album, News, Photo, Slider, Upload, Video', 0, N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime), NULL, NULL)

GO

INSERT [dbo].[Role] ([RoleID], [Name], [Description], [Type], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'11d14e1f-9da6-4b28-81a2-76751037d14e', N'ConferenceManager', N'/// Management items below: ConferenceDeclaration, ImportantDeadline, Presentation', 1, N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime), NULL, NULL)

GO

INSERT [dbo].[Role] ([RoleID], [Name], [Description], [Type], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'2ea7fd43-7704-430f-add6-4082c8a95d8d', N'Administrator', N'All resources', 2, N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime), NULL, NULL)

