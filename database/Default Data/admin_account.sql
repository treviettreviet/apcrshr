USE [APCRSHR]
GO

INSERT INTO [dbo].[Admin]
           ([AdminID]
           ,[FirstName]
           ,[LastName]
           ,[Email]
           ,[BirthDate]
           ,[Phone]
           ,[Locked]
           ,[UserName]
           ,[Password]
		   ,[Type])
     VALUES
           (N'03d48cb6-46b5-43d3-98f2-872eaacd36bc'
           , N'Mau Tien'
           , N'Doan'
           , 'tiendoan246@gmail.com'
           , '2012-12-12 00:00:00.000'
           , '0988888888'
           , 0
           , 'administrator'
           ,N'4MMHYVkmhqIlcZxTKZq7qa8ABQ0e5n0A0KBq7IK3nmpxHvSDUycXcuAr0ZFduZPLkTIFarzy3w=='
		   ,1)
GO

--User: administrator
--Password: admin


