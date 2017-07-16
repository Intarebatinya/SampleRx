-- RxDemoDb
-- This script does:
--		Create the dababase for the RxDemo application.
--		Initialize the database with sample data
-- Tested on SQL Server 2012 only.

USE master;
GO

IF NOT EXISTS(SELECT name FROM sys.databases WHERE name = 'RxDemo')
	BEGIN 
		PRINT N'Creating RxDemo...';
		CREATE DATABASE RxDemo;
	END
ELSE
	BEGIN 
		PRINT N'RxDemo already exists...'; 
	END
GO

PRINT N'Changin schema to RxDemo...';
USE RxDemo;
GO

PRINT N'Creating [dbo].[RxDataEntries]';
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RxDataEntries]') AND TYPE IN (N'U'))
BEGIN
	DROP TABLE [dbo].[RxDataEntries]
	PRINT N'Droping the existing [dbo].[RxDataEntries] first...'
END
GO

CREATE TABLE [dbo].[RxDataEntries]
(
	[EntryId] INT NOT NULL PRIMARY KEY, 
    [EntryCompanyId] INT NOT NULL, 
    [EntryDrugId] INT NOT NULL, 
    [Quantiy] INT NOT NULL, 
    [Price] DECIMAL(18, 2) NOT NULL
);

GO

PRINT N'Creating [dbo].[RxCompanyLogins]';
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RxCompanyLogins]') AND TYPE IN (N'U'))
BEGIN
	DROP TABLE [dbo].[RxCompanyLogins]
	PRINT N'Droping the existing [dbo].[RxCompanyLogins] first...'
END
GO

CREATE TABLE [dbo].[RxCompanyLogins]
(
	[RxLoginEmail] VARCHAR(50) NOT NULL PRIMARY KEY,
	[RxLoginCompanyId] INT NOT NULL,
	[RxLoginActive] BIT NOT NULL,
	[RxLoginHash] BINARY(64) NOT NULL	
);
GO

PRINT N'Creating [dbo].[RxCompanies]';
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RxCompanies]') AND TYPE IN (N'U'))
BEGIN
	DROP TABLE [dbo].[RxCompanies]
	PRINT N'Droping the existing [dbo].[RxCompanies] first...'
END
GO

CREATE TABLE [dbo].[RxCompanies]
(
	[RxCompanyId] INT NOT NULL PRIMARY KEY, 
    [RxCompanyName] VARCHAR(50) NOT NULL, 
    [RxCompanyActive] BIT NOT NULL, 
    [RxCompanyAccessKey] VARCHAR(32) NOT NULL
);

GO

PRINT N'Creating [dbo].[RxDrugs]';
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RxDrugs]') AND TYPE IN (N'U'))
BEGIN
	DROP TABLE [dbo].[RxDrugs]
	PRINT N'Droping the existing [dbo].[RxDrugs] first...'
END
GO

CREATE TABLE [dbo].[RxDrugs]
(
	[RxDrugId] INT NOT NULL PRIMARY KEY, 
    [RxDrugName] VARCHAR(50) NOT NULL
);

GO

-- Add contstaints
ALTER TABLE [dbo].[RxDataEntries]
ADD 
CONSTRAINT [FK_RxDataEntries_To_RxCompanies] FOREIGN KEY ([EntryCompanyId]) REFERENCES [RxCompanies]([RxCompanyId]), 
CONSTRAINT [FK_RxDataEntries_To_RxDrugs] FOREIGN KEY ([EntryDrugId]) REFERENCES [RxDrugs]([RxDrugId]);
GO

ALTER TABLE [dbo].[RxCompanyLogins]
ADD
CONSTRAINT [FK_RxComponyLogins_To_RxCompanies] FOREIGN KEY ([RxLoginCompanyId]) REFERENCES  [RxCompanies]([RxCompanyId]);
GO

-- Test Data

PRINT N'Inserting test data...';
GO
PRINT N'RxCompanies test data...';
DECLARE @cnt INT = 1;
WHILE @cnt <= 10    -- Adding 10 test companies
BEGIN
	INSERT INTO [dbo].[RxCompanies]([RxCompanyId], [RxCompanyName], [RxCompanyActive], [RxCompanyAccessKey])
						    VALUES (@cnt, 'Company_' + LTRIM(STR(@cnt)), 1, (select LOWER(REPLACE(NEWID(), '-', ''))));
	SET @cnt = @cnt + 1;
END

PRINT N'RxDrug test data...';
SET @cnt = 1;
WHILE @cnt <= 100		-- Adding 100 test drugs
BEGIN
	INSERT INTO [dbo].[RxDrugs]([RxDrugId], [RxDrugName])
						VALUES(@cnt, 'Drug_' + LTRIM(STR(@cnt)));
	SET @cnt = @cnt + 1;
END

PRINT N'RxDataEntries test data...';
SET @cnt = 1;
WHILE @cnt < = 100000		--Adding 1000000 test prescription entries
BEGIN 
	INSERT INTO [dbo].[RxDataEntries]([EntryId], [EntryCompanyId], [EntryDrugId], [Quantiy], [Price])
							VALUES (@cnt,
									FLOOR(RAND()*(10-1)+1),			-- Random company among the ones inserted above
									FLOOR(RAND()*(100-1)+1),		-- Random Drug among the ones inserted above
									FLOOR(RAND()*(300-1)+1),		-- Assuming max qt is 300
									ROUND((RAND()*(10000-1)+1), 0))	-- Assuming max price is 10000	
	SET @cnt = @cnt + 1;
END
PRINT N'DONE...';