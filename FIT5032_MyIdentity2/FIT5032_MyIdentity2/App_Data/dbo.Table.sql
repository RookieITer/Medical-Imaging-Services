CREATE TABLE [dbo].[Students] (
	[Id] INT IDENTITY (1,1) NOT NULL,
	[FirstName] nvarchar (max) NOT NULL,
	[LastName] nvarchar (max) NOT NULL,
		[UserID] nvarchar (max) NOT NULL
	PRIMARY KEY (Id)
);
GO
