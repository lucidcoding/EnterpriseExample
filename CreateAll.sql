USE [Master]

IF EXISTS (SELECT * FROM sys.databases WHERE NAME = 'HumanResources')
BEGIN
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'HumanResources'
	ALTER DATABASE [HumanResources] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE [HumanResources]
END

IF EXISTS (SELECT * FROM sys.databases WHERE NAME = 'Sales')
BEGIN
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'Sales'
	ALTER DATABASE [Sales] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE [Sales]
END

IF EXISTS (SELECT * FROM sys.databases WHERE NAME = 'ClientServices')
BEGIN
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'ClientServices'
	ALTER DATABASE [ClientServices] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE [ClientServices]
END

IF EXISTS (SELECT * FROM sys.databases WHERE NAME = 'Finance')
BEGIN
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'Finance'
	ALTER DATABASE [Finance] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE [Finance]
END

CREATE DATABASE [HumanResources] 
GO

CREATE DATABASE [Sales] 
GO

CREATE DATABASE [ClientServices]
GO

CREATE DATABASE [Finance]
GO

USE [HumanResources]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Department')
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
	INSERT INTO [Department] ([Id], [Name]) VALUES ('42c25da5-7c04-4adc-a9c2-6bf8a9ff5c89', 'Client Services')
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Employee')
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
	INSERT INTO [Employee] ([Id], [Forename], [Surname], [Joined], [Left], [HolidayEntitlement], [DepartmentId]) VALUES ('2ea69309-0818-4b40-a740-fe22e2d4dfcb', 'Gary', 'Green', '2012-03-01 00:00:00', NULL, 23, '42c25da5-7c04-4adc-a9c2-6bf8a9ff5c89')
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Holiday')
BEGIN
	CREATE TABLE [dbo].[Holiday](
		[Id] uniqueidentifier NOT NULL,
		[EmployeeId] uniqueidentifier NOT NULL,
		[Start] datetime NULL,
		[End] datetime NULL,
		[Description] nvarchar(100) NULL
		CONSTRAINT [PK_Holiday] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

USE [Sales]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Visit')
BEGIN
	CREATE TABLE [dbo].[Visit](
		[Id] uniqueidentifier NOT NULL,
		[ConsultantId] uniqueidentifier NULL,
		[Start] datetime  NOT NULL,
		[End] datetime NOT NULL,
		[LeadId] uniqueidentifier NULL,
		[Completed] bit NOT NULL,
		CONSTRAINT [PK_Visit] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Lead')
BEGIN
	CREATE TABLE [dbo].[Lead](
		[Id] uniqueidentifier NOT NULL,
		[Name] nvarchar(100) NULL,
		[Address1] nvarchar(100) NULL,
		[Address2] nvarchar(100) NULL,
		[Address3] nvarchar(100) NULL,
		[PhoneNumber] nvarchar(100) NULL,
		[AssignedToConsultantId] uniqueidentifier NULL,
		[SignedUp] bit NOT NULL,
		CONSTRAINT [PK_Lead] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	INSERT INTO [Lead] ([Id], [Name], [Address1], [Address2], [Address3], [PhoneNumber], [AssignedToConsultantId], [SignedUp]) VALUES ('c81a69b9-40be-4553-abbf-e334b64e5f8a', 'The Orange Company', '6 Orange Road', 'Orangeborough', 'Orangeshire', '01234 567890', '54b26de9-2dae-4168-a66c-281b6f03f1b5', 0)
	INSERT INTO [Lead] ([Id], [Name], [Address1], [Address2], [Address3], [PhoneNumber], [AssignedToConsultantId], [SignedUp]) VALUES ('f346bcc5-b2d1-4b4e-9359-f810d1880fcb', 'Purple Inc.', '1 Purple Street', 'Purpleton', 'Purpleshire', '07890 123456', '54b26de9-2dae-4168-a66c-281b6f03f1b5', 0)
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Deal')
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

USE [ClientServices]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Client')
BEGIN
	CREATE TABLE [dbo].[Client](
		[Id] uniqueidentifier NOT NULL,
		[Name] nvarchar(100) NULL,
		[Reference] nvarchar(100) NULL,
		[Address1] nvarchar(100) NULL,
		[Address2] nvarchar(100) NULL,
		[Address3] nvarchar(100) NULL,
		[PhoneNumber] nvarchar(100) NULL,
		[LiasonEmployeeId] uniqueidentifier NULL,
		[CurrentAgreementId] uniqueidentifier NULL
		CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Service')
BEGIN
	CREATE TABLE [dbo].[Service](
		[Id] uniqueidentifier NOT NULL,
		[Name] nvarchar(100) NULL
		CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	INSERT INTO [Service] ([Id], [Name]) VALUES ('d83db4f2-940c-4c26-bc7c-8ec2f5bbb994', 'Service 1')
	INSERT INTO [Service] ([Id], [Name]) VALUES ('80650c0e-7b7f-438b-9fd0-8efa303d72b4', 'Service 2')
	INSERT INTO [Service] ([Id], [Name]) VALUES ('3ae2c980-b88e-4401-bedc-20e8a8da14b2', 'Service 3')
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Agreement')
BEGIN
	CREATE TABLE [dbo].[Agreement](
		[Id] uniqueidentifier NOT NULL,
		[ClientId] uniqueidentifier NULL,
		[DealId] uniqueidentifier NULL,
		[Commencement] datetime NULL,
		[Expiry] datetime NULL,
		[Status] int NULL
		CONSTRAINT [PK_Agreement] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AgreementService')
BEGIN
	CREATE TABLE [dbo].[AgreementService](
		[AgreementId] uniqueidentifier NOT NULL,
		[ServiceId] uniqueidentifier NOT NULL
		CONSTRAINT [PK_AgreementService] PRIMARY KEY CLUSTERED 
		(
			[AgreementId] ASC,
			[ServiceId] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

USE [Finance]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Account')
BEGIN
	CREATE TABLE [dbo].[Account](
		[Id] uniqueidentifier NOT NULL,
		[ClientId] uniqueidentifier NULL,
		[AgreementId] uniqueidentifier NULL,
		[Expiry] datetime NULL,
		[Value] int NULL,
		[Status] int NULL
		CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Installment')
BEGIN
	CREATE TABLE [dbo].[Installment](
		[Id] uniqueidentifier NOT NULL,
		[AccountId] uniqueidentifier NULL,
		[DueDate] datetime NULL,
		[Amount] int NULL,
		[Paid] bit NULL
		CONSTRAINT [PK_Installment] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO