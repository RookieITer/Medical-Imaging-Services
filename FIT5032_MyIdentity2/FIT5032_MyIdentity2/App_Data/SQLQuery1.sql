-- Create table 'Students'
CREATE TABLE [dbo].[Students] (
	[Id] INT IDENTITY (1,1) NOT NULL,
	[FirstName] nvarchar (max) NOT NULL,
	[LastName] nvarchar (max) NOT NULL,
		[UserID] nvarchar (max) NOT NULL
	PRIMARY KEY (Id)
);
GO

-- Create table 'Units'
CREATE TABLE [dbo].[Units] (
	[Id] INT IDENTITY (1,1) NOT NULL,
	[Name] nvarchar (max) NOT NULL,
	[Description] nvarchar (max) NOT NULL,
		[StudentId] int NOT NULL
	PRIMARY KEY (Id),
	FOREIGN KEY (StudentId) REFERENCES Students (Id)
);
GO