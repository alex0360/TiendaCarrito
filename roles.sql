--https://youtu.be/EFxkYjN4670?t=8m3s
Insert into [dbo].[AspNetRoles]
(Id,Name)
Values
(2,'Usario')
Go
--https://youtu.be/EFxkYjN4670?t=8m43s
Declare @Id nvarchar(128)
Select @Id=Id
From AspNetUsers
Where UserName = 'Admin'

Insert Into AspNetUserRoles
([UserId],[RoleId])
Values
(@Id,1)