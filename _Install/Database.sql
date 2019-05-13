CREATE SCHEMA core;

GO

CREATE TABLE core.tUser
(
	Id int NOT NULL IDENTITY(1, 1) CONSTRAINT Pk_User PRIMARY KEY,
	Username nvarchar(100) NOT NULL,
	Password nvarchar(100) NOT NULL,
	Firstname nvarchar(100) NOT NULL,
	Lastname nvarchar(100) NOT NULL,
	Email nvarchar(250) NOT NULL,
	IsAdmin bit NOT NULL CONSTRAINT Df_User_IsAdmin DEFAULT (0),
	DeletedAt datetime NULL
);

GO

CREATE TABLE core.tProject
(
	Id int NOT NULL IDENTITY(1, 1) CONSTRAINT Pk_Project PRIMARY KEY,
	Code nvarchar(100) NOT NULL,
	YouTrackUrl nvarchar(100) NOT NULL,
	YouTrackQuery nvarchar(100) NOT NULL,
	DeletedAt datetime NULL
);

GO

CREATE TABLE core.tRole
(
	Id int NOT NULL CONSTRAINT Pk_Role PRIMARY KEY,
	Code nvarchar(50) NOT NULL CONSTRAINT Uq_Role_Code UNIQUE
);

GO

INSERT INTO core.tRole (Id, Code)
VALUES (1, 'Team lead'),
       (2, 'Team member');

GO

CREATE TABLE core.tProjectUser
(
	Id int NOT NULL IDENTITY(1, 1) CONSTRAINT Pk_ProjectUser PRIMARY KEY,
	Fk_User_Id int NOT NULL CONSTRAINT Fk_ProjectUser_User FOREIGN KEY REFERENCES core.tUser (Id),
	Fk_Project_Id int NOT NULL CONSTRAINT Fk_ProjectUser_Project FOREIGN KEY REFERENCES core.tProject (Id),
	Fk_Role_Id int NOT NULL CONSTRAINT Fk_ProjectUser_Role FOREIGN KEY REFERENCES core.tRole (Id),
	DeletedAt datetime NULL
);

GO

CREATE TABLE core.tIssueEstimation
(
	Id int NOT NULL CONSTRAINT Pk_IssueEstimation PRIMARY KEY,
	Fk_ProjectUser_Id int NOT NULL CONSTRAINT Fk_IssueEstimation_ProjectUser FOREIGN KEY REFERENCES core.tProjectUser (Id),
	Issue nvarchar(100) NOT NULL,
	Estimation int NOT NULL,
	CreatedAt datetime NOT NULL,
	IsFinal bit NOT NULL CONSTRAINT Df_IssueEstimation_IsFinal DEFAULT (1)
);

GO