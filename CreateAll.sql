USE [Master]

IF EXISTS (SELECT * FROM sysdatabases WHERE name='HumanResources') 
BEGIN 
	DROP DATABASE [HumanResources] 
END
GO

IF EXISTS (SELECT * FROM sysdatabases WHERE name='Sales') 
BEGIN 
	DROP DATABASE [Sales] 
END
GO

CREATE DATABASE [HumanResources] 
GO

CREATE DATABASE [Sales] 
GO

USE [HumanResources]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Department')
BEGIN
	CREATE TABLE [dbo].[Department](
		[Id] uniqueidentifier NOT NULL,
		[Name] nvarchar(100) NULL
		CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	INSERT INTO [Department] ([Id], [Name]) VALUES ('30efbfda-f3b5-42fb-906e-098fb32be79d', 'Sales')
	INSERT INTO [Department] ([Id], [Name]) VALUES ('f7367187-406d-47db-931e-b9e4fa8a4774', 'Human Resources')
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Employee')
BEGIN
	CREATE TABLE [dbo].[Employee](
		[Id] uniqueidentifier NOT NULL,
		[Forename] nvarchar(100) NULL,
		[Surname] nvarchar(100) NULL,
		[Joined] datetime NULL,
		[Left] datetime NULL,
		[HolidayEntitlement] int NULL,
		[DepartmentId] uniqueidentifier NULL
		CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	INSERT INTO [Employee] ([Id], [Forename], [Surname], [Joined], [Left], [HolidayEntitlement], [DepartmentId]) VALUES ('54b26de9-2dae-4168-a66c-281b6f03f1b5', 'Barry', 'Blue', '2012-01-01 00:00:00', NULL, 21, '30efbfda-f3b5-42fb-906e-098fb32be79d')
	INSERT INTO [Employee] ([Id], [Forename], [Surname], [Joined], [Left], [HolidayEntitlement], [DepartmentId]) VALUES ('4f738440-258f-4539-8ac0-387836815361', 'Rachel', 'Red', '2012-03-01 00:00:00', NULL, 23, 'f7367187-406d-47db-931e-b9e4fa8a4774')
END
GO

USE [Sales]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Visit')
BEGIN
	CREATE TABLE [dbo].[Visit](
		[Id] uniqueidentifier NOT NULL,
		[ConsultantId] uniqueidentifier NULL,
		[Start] datetime  NOT NULL,
		[End] datetime NOT NULL,
		[LeadId] uniqueidentifier NULL
		CONSTRAINT [PK_Visit] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	INSERT INTO [Visit] ([Id], [Start], [End], [LeadId]) VALUES ('c81a69b9-40be-4553-abbf-e334b64e5f8a', '2012-08-07 10:00:00', '2012-08-07 11:00:00', 'c81a69b9-40be-4553-abbf-e334b64e5f8a')
	INSERT INTO [Visit] ([Id], [Start], [End], [LeadId]) VALUES ('f346bcc5-b2d1-4b4e-9359-f810d1880fcb', '2012-08-07 10:00:00', '2012-08-07 11:00:00', 'f346bcc5-b2d1-4b4e-9359-f810d1880fcb')
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Lead')
BEGIN
	CREATE TABLE [dbo].[Lead](
		[Id] uniqueidentifier NOT NULL,
		[Name] nvarchar(100) NULL,
		[Address1] nvarchar(100) NULL,
		[Address2] nvarchar(100) NULL,
		[Address3] nvarchar(100) NULL,
		[AssignedToConsultantId] uniqueidentifier NULL,
		[SignedUp] bit NOT NULL,
		CONSTRAINT [PK_Lead] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	INSERT INTO [Lead] ([Id], [Name], [Address1], [Address2], [AssignedToConsultantId], [SignedUp]) VALUES ('c81a69b9-40be-4553-abbf-e334b64e5f8a', 'The Orange Company', '6 Orange Road, Orangeborough', 'Orangeshire', '54b26de9-2dae-4168-a66c-281b6f03f1b5', 0)
	INSERT INTO [Lead] ([Id], [Name], [Address1], [Address2], [AssignedToConsultantId], [SignedUp]) VALUES ('f346bcc5-b2d1-4b4e-9359-f810d1880fcb', 'Purple Inc.', '1 Purple Street, Purpleton', 'Purpleshire', '54b26de9-2dae-4168-a66c-281b6f03f1b5', 0)
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Deal')
BEGIN
	CREATE TABLE [dbo].[Deal](
		[Id] uniqueidentifier NOT NULL,
		[LeadId] uniqueidentifier NULL,
		[MadeByConsultantId] uniqueidentifier NULL,
		[Value] int NULL,
		[Commission] int NULL
		CONSTRAINT [PK_Deal] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'DealService')
BEGIN
	CREATE TABLE [dbo].[DealService](
		[Id] uniqueidentifier NOT NULL,
		[DealId] uniqueidentifier NULL,
		[ServiceId] uniqueidentifier NULL
		CONSTRAINT [PK_DealService] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO