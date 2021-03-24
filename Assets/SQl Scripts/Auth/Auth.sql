INSERT INTO AspNetRoles (Id, Name, NormalizedName)
VALUES
('User', 'User', 'User'),
('Admin', 'Admin', 'Admin');


INSERT INTO AspNetUserRoles (UserId, RoleId)
SELECT Id, 'User' AS RoleId
FROM AspNetUsers

INSERT INTO AspNetUserRoles (UserId, RoleId)
SELECT Id, 'Admin' AS RoleId
FROM AspNetUsers
WHERE UserName = 'bob'