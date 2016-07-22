USE [APCRSHR]
GO

INSERT INTO [dbo].[User]
           ([UserID]
           ,[Title]
           ,[FullName]
           ,[Sex]
           ,[Email]
           ,[DateOfBirth]
           ,[PhoneNumber]
           ,[UserName]
           ,[Password]
           ,[CreatedDate]
           ,[Locked]
           ,[Address]
           ,[City]
           ,[Country]
           ,[RegistrationStatus])
     VALUES
           ('72e775cf-2a9c-4234-bb1b-25a7c1eb3ba9'
           ,'MR'
           ,N'Mau Tien Doan'
           ,'Male'
           ,'tiendoan246@gmail.com'
           , '2012-12-12 00:00:00.000'
           , '0988888888'
           ,'tiendoan246@gmail.com'
           ,N'4MMHYVkmhqIlcZxTKZq7qa8ABQ0e5n0A0KBq7IK3nmpxHvSDUycXcuAr0ZFduZPLkTIFarzy3w=='
           , '2012-12-12 00:00:00.000'
           ,0
		   ,N'85. Manchester stress'
           ,'London'
           ,'United Kingdom'
           ,0)
GO


