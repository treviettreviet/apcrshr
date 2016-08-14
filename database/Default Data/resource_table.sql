USE [APCRSHR]

GO

--Album

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'ccd35368-3d89-4c10-a754-03d467b74a1c', N'Danh sách album', N'AdminAlbum/Index', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'f359393f-8fff-4cd5-82a1-ea704eea4ca6', N'Tạo mới album', N'AdminAlbum/CreateAlbum', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'b4809b9a-be29-4e38-9f02-fc6ed40df108', N'Cập nhập album', N'AdminAlbum/UpdateAlbum', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

--Đăng ký hội nghị

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'a4c9f975-d2cf-44cd-8c4f-cc1ca6b2d23d', N'Danh sách đăng ký hội nghị', N'AdminConferenceDeclare/Index', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'133a0bdf-8fb6-460e-86dd-0ee0922b0acc', N'Đăng ký hội nghị', N'AdminConferenceDeclare/CreateConference', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'70256413-ea1f-4665-be3f-ecb49885f31b', N'Cập nhập hội nghị', N'AdminConferenceDeclare/UpdateConference', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

--Administrator

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'045aedb0-21bf-45c4-a5bc-67b2d5c8fe5e', N'Trang chủ quản trị', N'Admin/Index', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'ea988f7a-d97a-44b6-ac8e-190ec063e784', N'Danh sách quản trị', N'Admin/AdminList', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'082d2b7f-2dd1-46d6-a6ee-ce7f002cd943', N'Thêm mới người quản trị', N'Admin/AddNewAdmin', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'd5a066ae-3a1b-4d25-8752-9b29582940fe', N'Cập nhập thông người quản trị', N'Admin/UpdateAdmin', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'aefcdbcf-a66d-4fac-ac8e-2c51e98f92d1', N'Xóa người quản trị', N'Admin/DeleteAdmin', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'2a20d44a-e5e3-4299-a9b1-12aa98de9729', N'Thêm mới menu', N'Admin/CreateCategoryMenu', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'620fdbcd-2e04-412c-8a03-6240bc3a8001', N'Tải file lên server', N'Admin/UploadFiles', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'1f4c27a3-e1ba-40d3-a93f-716b87002a21', N'Xóa file đã upload', N'Admin/DeleteUpload', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'd2fc6863-188c-4fbf-b363-95e0dd8862ad', N'Cập nhập menu', N'Admin/UpdateCategory', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'8fbca43a-cd86-4e3a-aaf0-805d037b1584', N'Xóa menu', N'Admin/DeleteMenu', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

--AdminImportant Deadline

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'3fe418bb-7bbc-4924-a45f-e96e7a0eb99c', N'Danh sách Important Deadline', N'AdminImportantDeadline/Index', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'e5b3e36d-db93-4156-895d-c72f8e576a12', N'Thêm mới Important Deadline', N'AdminImportantDeadline/CreateImportantDeadline', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'685f6e9e-1a5f-425c-9c3d-4a1ee28412e6', N'Xóa Important Deadline', N'AdminImportantDeadline/DeleteImportantDealine', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'0cadb228-72fb-43b7-ba0d-c318e611c645', N'Cập nhập thông tin Important Deadline', N'AdminImportantDeadline/UpdateImportantDeadline', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

--News

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'73a3f0d3-60bc-45c5-8bd5-c250641f783a', N'Danh sách tin tức', N'AdminNews/Index', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'd417a0d1-14a2-4c19-8098-79253e119884', N'Thêm mới tin tức', N'AdminNews/CreateNews', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'87c49ae0-e632-4f73-9a7f-7d0f3c07c257', N'Xóa tin tức', N'AdminNews/DeleteNews', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'177aecb3-6bb4-47d7-9da1-54f862a666df', N'Cập nhập thông tin tin tức', N'AdminNews/UpdateNews', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

-- Photo
--TODO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'28ed0f07-d4c5-4a03-9efd-2c2ec0ed3958', N'Danh sách ảnh', N'AdminPhoto/Index', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

--Presentation

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'a0dae0e8-d803-4e0a-9f02-86c3bdff6b96', N'Danh dữ liệu trình bày', N'AdminPresentation/Index', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'20d186c9-af06-41c2-9f05-69b8395416ee', N'Thêm mới dữ liệu trình bày', N'AdminPresentation/CreatePresentation', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'8dfe6d9a-1d23-4c46-b0fd-61c484f93b3f', N'Cập nhập dữ liệu trình bày', N'AdminPresentation/UpdatePresentation', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'30ff1c82-7b4c-4d11-ae14-7569243670fc', N'Xóa dữ liệu trình bày', N'AdminPresentation/DeletePresentation', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

--Role
--TODO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'031a8ed5-c249-4305-9d73-6b6618bff419', N'Danh sách phân quyền', N'AdminRole/Index', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'3704eed9-68ca-4d20-9217-9cfc8cf86d27', N'Phân quyền', N'AdminRole/AssignRole', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

--User

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'f994a4ab-d4fc-4276-abb8-11881058e5ec', N'Danh sách người dùng', N'AdminUser/Index', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'997c4c27-eebc-4e25-addc-46ed8d5700a7', N'Cập nhập người dùng', N'AdminUser/UpdateUser', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'4a2a3e38-9027-4a00-a700-7770ae41a415', N'Xem thông tin người dùng', N'AdminUser/ViewUser', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'5f02e317-930d-4459-8b0b-1202e4c1b0bd', N'Xóa người dùng', N'AdminUser/DeleteUser', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

--Video

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'76c4987e-47a4-414b-9777-38e0ed2634b1', N'Danh sách video', N'AdminVideo/Index', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'74b33004-76d9-4f44-9279-6c32fbd99abd', N'Thêm mới video', N'AdminVideo/CreateVideo', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'6186c355-ad11-4680-b774-58f27293eccf', N'Cập nhập video', N'AdminVideo/UpdateVideo', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'15cc957e-8d7e-4c62-a09b-f9b75ae42ef6', N'Xóa video', N'AdminVideo/deleteVideo', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

--Article

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'4ef2d5c0-2cb8-488e-a165-4b073ee893a7', N'Danh sách bài viết', N'Article/Index', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'cf1bf6f5-6793-4f42-822e-013841e76d9b', N'Thêm bài viết', N'Article/CreateArticle', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'708c7223-75b4-48e4-bbce-64d247bc2409', N'Xóa bài viết', N'Article/DeleteArticle', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO

INSERT [dbo].[Resource] ([ResourceID], [Title], [URL], [CreatedBy], [CreatedDate]) VALUES (N'0d09d4df-8bca-4512-a300-e6dc8d64473d', N'Cập nhập bài viết', N'Article/UpdateArticle', N'03d48cb6-46b5-43d3-98f2-872eaacd36bc', CAST(0x0000A12500000000 AS DateTime))

GO