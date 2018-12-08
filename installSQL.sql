create table Roles (
	RoleId int CONSTRAINT PK_RoleId PRIMARY KEY IDENTITY(1,1),
	RoleName nvarchar(25) NOT NULL
);

create table Users (
	UserId int CONSTRAINT PK_UserId PRIMARY KEY IDENTITY(1,1),
	UserName nvarchar(25) NOT NULL,
	UserLogin nvarchar(25) NOT NULL,
	Password nvarchar(25) NOT NULL,
	RoleId int NOT NULL,
	CONSTRAINT FK_Users_To_Roles FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);

create table Logs (
	LogId int CONSTRAINT PK_LogId PRIMARY KEY IDENTITY(1,1),
	DataEnter DateTime NOT NULL,
	Issuccess bit NOT NULL,
	UserId int NOT NULL,
	CONSTRAINT FK_Logs_To_Users FOREIGN KEY (UserId) REFERENCES Users (UserId)
);
	
create table Message (
	MessageId int CONSTRAINT PK_MessageId PRIMARY KEY IDENTITY(1,1),
	Subject nvarchar(25) NOT NULL,
	Body nvarchar(MAX) NOT NULL,
	Date DateTime NOT NULL,
	AuthorId int NOT NULL,
	CONSTRAINT FK_Message_To_Users FOREIGN KEY (AuthorId) REFERENCES Users (UserId) 
);

create table MessagePlaceHolders(
	PlaceHolderId int CONSTRAINT PK_PlaceHolderId PRIMARY KEY IDENTITY(1,1),
	PlaceHolder nvarchar(25) NOT NULL
);

create table Users_Messages_Mapped (
	Users_Messages_MappedId int CONSTRAINT PK_Users_Messages_MappedId PRIMARY KEY IDENTITY(1,1),
	MessageId int NOT NULL ,
	UserId int NOT NULL,
	PlaceHolderId int NOT NULL,
	IsRead bit NOT NULL,
	IsStarred bit NOT NULL
	CONSTRAINT FK_User_Messages_Mappe_To_Messages FOREIGN KEY (MessageId) REFERENCES Message (MessageId),  
	CONSTRAINT FK_User_Messages_Mappe_To_Users FOREIGN KEY (UserId) REFERENCES Users (Userid),
	CONSTRAINT FK_User_Messages_Mappe_To_MessagePlaceHolders FOREIGN KEY (PlaceHolderId) REFERENCES MessagePlaceHolders (PlaceHolderId)
);



--drop table Users_Messages_Mapped
--drop table MessagePlaceHolders
--drop table Message
--drop table Logs
--drop table Users
--drop table Roles

--drop procedure GetUser

create Procedure GetUser
    (@Login Varchar(25), @Password Varchar(25))
As
Begin
    select * from Users u
		where @Login=u.UserLogin and @Password=u.Password
End

select *  from users
