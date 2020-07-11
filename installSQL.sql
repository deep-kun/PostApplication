CREATE DATABASE [PostService]
GO
USE [PostService]

CREATE TABLE [Roles] (
	[RoleId] int CONSTRAINT PK_RoleId PRIMARY KEY IDENTITY(1,1),
	[RoleName] nvarchar(25) NOT NULL
);

CREATE TABLE [Users] (
	[UserId] int CONSTRAINT PK_UserId PRIMARY KEY IDENTITY(1,1),
	[UserName] nvarchar(50) NOT NULL,
	[UserLogin] nvarchar(50) NOT NULL UNIQUE,
	[PasswordHash] nvarchar(64) NOT NULL,
	[RoleId] int NOT NULL,
	CONSTRAINT FK_Users_To_Roles FOREIGN KEY ([RoleId]) REFERENCES Roles([RoleId]),
);

CREATE TABLE [Messages] (
	[MessageId] int CONSTRAINT PK_MessageId PRIMARY KEY IDENTITY(1,1),
	[Subject] nvarchar(25) NOT NULL,
	[Body] nvarchar(max) NOT NULL,
	[SentDate] datetimeoffset  NOT NULL,
	[AuthorId] int NOT NULL,
	CONSTRAINT FK_Message_To_Users FOREIGN KEY ([AuthorId]) REFERENCES [Users] ([UserId]) 
);

CREATE TABLE [MessagePlaceHolders] (
	[PlaceHolderId] int CONSTRAINT PK_PlaceHolderId PRIMARY KEY IDENTITY(1,1),
	[PlaceHolder] nvarchar(25) NOT NULL
);

CREATE TABLE [UsersMessagesMapped] (
	[UsersMessagesMappedId] int CONSTRAINT PKUsersMessagesMappedId PRIMARY KEY IDENTITY(1,1),
	[MessageId] int NOT NULL,
	[UserId] int NOT NULL,
	[PlaceHolderId] int NOT NULL,
	[IsRead] bit NOT NULL,
	[IsStarred] bit NOT NULL
	CONSTRAINT FK_User_Messages_Mappe_To_Messages FOREIGN KEY (MessageId) REFERENCES Messages ([MessageId]),  
	CONSTRAINT FK_User_Messages_Mappe_To_Users FOREIGN KEY (UserId) REFERENCES Users ([UserId]),
	CONSTRAINT FK_User_Messages_Mappe_To_MessagePlaceHolders FOREIGN KEY ([PlaceHolderId]) REFERENCES [MessagePlaceHolders] ([PlaceHolderId])
);

GO
CREATE PROCEDURE GetUserByLoginAndPassword
    (@Login nvarchar(50), @PasswordHash nvarchar(64))
As
Begin
    select 
	[UserId],
	[UserName], 
	[UserId],
	[UserLogin],
	[PasswordHash],
	[RoleId] from Users u
		where @Login=u.UserLogin and @PasswordHash=u.PasswordHash
End

print('Database created successufuly.')

INSERT INTO [Roles] VALUES ('User')

print('Initial data created successufuly.')