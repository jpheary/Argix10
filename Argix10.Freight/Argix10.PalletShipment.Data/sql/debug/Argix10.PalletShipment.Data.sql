/*
Deployment script for Argix10.PalletShipment.Data
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Argix10.PalletShipment.Data"
:setvar DefaultDataPath ""
:setvar DefaultLogPath ""

GO
:on error exit
GO
USE [master]
GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL
    AND DATABASEPROPERTYEX(N'$(DatabaseName)','Status') <> N'ONLINE')
BEGIN
    RAISERROR(N'The state of the target database, %s, is not set to ONLINE. To deploy to this database, its state must be set to ONLINE.', 16, 127,N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)] COLLATE SQL_Latin1_General_CP1_CI_AS
GO
EXECUTE sp_dbcmptlevel [$(DatabaseName)], 100;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
USE [$(DatabaseName)]
GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
PRINT N'Creating [dbo].[ltlClient]...';


GO
CREATE TABLE [dbo].[ltlClient] (
    [ID]                    INT          IDENTITY (100000, 1) NOT NULL,
    [Name]                  VARCHAR (40) NOT NULL,
    [AddressLine1]          VARCHAR (40) NULL,
    [AddressLine2]          VARCHAR (40) NULL,
    [City]                  VARCHAR (40) NULL,
    [State]                 CHAR (2)     NULL,
    [Zip]                   CHAR (5)     NULL,
    [Zip4]                  CHAR (4)     NULL,
    [ContactName]           VARCHAR (40) NOT NULL,
    [ContactPhone]          VARCHAR (24) NULL,
    [ContactEmail]          VARCHAR (50) NOT NULL,
    [CorporateName]         VARCHAR (40) NULL,
    [CorporateAddressLine1] VARCHAR (40) NULL,
    [CorporateAddressLine2] VARCHAR (40) NULL,
    [CorporateCity]         VARCHAR (40) NULL,
    [CorporateState]        CHAR (2)     NULL,
    [CorporateZip]          CHAR (5)     NULL,
    [CorporateZip4]         CHAR (4)     NULL,
    [TaxIDNumber]           CHAR (10)    NULL,
    [BillingAddressLine1]   VARCHAR (40) NULL,
    [BillingAddressLine2]   VARCHAR (40) NULL,
    [BillingCity]           VARCHAR (40) NULL,
    [BillingState]          CHAR (2)     NULL,
    [BillingZip]            CHAR (5)     NULL,
    [BillingZip4]           CHAR (4)     NULL,
    [Number]                CHAR (3)     NULL,
    [SalesRepClientID]      INT          NULL,
    [Status]                CHAR (1)     NULL,
    [Approved]              BIT          NULL,
    [ApprovedDate]          DATETIME     NULL,
    [ApprovedUser]          VARCHAR (50) NULL,
    [LastUpdated]           DATETIME     NOT NULL,
    [UserID]                VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ltlClient] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[ltlConsignee]...';


GO
CREATE TABLE [dbo].[ltlConsignee] (
    [ID]              INT          IDENTITY (100000, 1) NOT NULL,
    [ClientID]        INT          NOT NULL,
    [Name]            VARCHAR (40) NOT NULL,
    [AddressLine1]    VARCHAR (40) NOT NULL,
    [AddressLine2]    VARCHAR (40) NULL,
    [City]            VARCHAR (40) NOT NULL,
    [State]           CHAR (2)     NOT NULL,
    [Zip]             CHAR (5)     NOT NULL,
    [Zip4]            CHAR (4)     NULL,
    [ContactName]     VARCHAR (40) NOT NULL,
    [ContactPhone]    VARCHAR (24) NOT NULL,
    [ContactEmail]    VARCHAR (50) NOT NULL,
    [WindowStartTime] DATETIME     NULL,
    [WindowEndTime]   DATETIME     NULL,
    [Status]          CHAR (1)     NOT NULL,
    [LastUpdated]     DATETIME     NOT NULL,
    [UserID]          VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ltlConsignee] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[ltlPallet]...';


GO
CREATE TABLE [dbo].[ltlPallet] (
    [ID]             INT            IDENTITY (100000, 1) NOT NULL,
    [PalletNumber]   VARCHAR (11)   NOT NULL,
    [ShipmentID]     INT            NOT NULL,
    [Weight]         INT            NOT NULL,
    [NMFCClass]      VARCHAR (5)    NULL,
    [InsuranceValue] DECIMAL (9, 2) NULL,
    CONSTRAINT [PK_ltlPallet] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[ltlQuoteLog]...';


GO
CREATE TABLE [dbo].[ltlQuoteLog] (
    [ID]                     INT            IDENTITY (100000, 1) NOT NULL,
    [Created]                DATETIME       NOT NULL,
    [ShipDate]               DATETIME       NOT NULL,
    [OriginZip]              VARCHAR (5)    NOT NULL,
    [DestinationZip]         VARCHAR (5)    NOT NULL,
    [Pallet1Weight]          INT            NOT NULL,
    [Pallet1Class]           VARCHAR (5)    NOT NULL,
    [Pallet1InsuranceValue]  DECIMAL (9, 2) NULL,
    [Pallet2Weight]          INT            NULL,
    [Pallet2Class]           VARCHAR (5)    NULL,
    [Pallet2InsuranceValue]  DECIMAL (9, 2) NULL,
    [Pallet3Weight]          INT            NULL,
    [Pallet3Class]           VARCHAR (5)    NULL,
    [Pallet3InsuranceValue]  DECIMAL (9, 2) NULL,
    [Pallet4Weight]          INT            NULL,
    [Pallet4Class]           VARCHAR (5)    NULL,
    [Pallet4InsuranceValue]  DECIMAL (9, 2) NULL,
    [Pallet5Weight]          INT            NULL,
    [Pallet5Class]           VARCHAR (5)    NULL,
    [Pallet5InsuranceValue]  DECIMAL (9, 2) NULL,
    [InsidePickup]           BIT            NULL,
    [LiftGateOrigin]         BIT            NULL,
    [AppointmentOrigin]      BIT            NULL,
    [InsideDelivery]         BIT            NULL,
    [LiftGateDestination]    BIT            NULL,
    [AppointmentDestination] BIT            NULL,
    [Pallets]                INT            NOT NULL,
    [Weight]                 DECIMAL (9, 2) NOT NULL,
    [PalletRate]             DECIMAL (9, 2) NOT NULL,
    [FuelSurcharge]          DECIMAL (9, 2) NOT NULL,
    [AccessorialCharge]      DECIMAL (9, 2) NOT NULL,
    [InsuranceCharge]        DECIMAL (9, 2) NOT NULL,
    [TollCharge]             DECIMAL (9, 2) NOT NULL,
    [TotalCharge]            DECIMAL (9, 2) NOT NULL,
    CONSTRAINT [PK_ltlQuoteLog] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[ltlShipment]...';


GO
CREATE TABLE [dbo].[ltlShipment] (
    [ID]                     INT            IDENTITY (100000, 1) NOT NULL,
    [ShipmentNumber]         VARCHAR (9)    NOT NULL,
    [Created]                DATETIME       NOT NULL,
    [ClientID]               INT            NOT NULL,
    [ShipDate]               DATETIME       NOT NULL,
    [ShipperID]              INT            NOT NULL,
    [ConsigneeID]            INT            NOT NULL,
    [Pallets]                INT            NOT NULL,
    [Weight]                 DECIMAL (9, 2) NOT NULL,
    [InsidePickup]           BIT            NULL,
    [LiftGateOrigin]         BIT            NULL,
    [AppointmentOrigin]      DATETIME       NULL,
    [InsideDelivery]         BIT            NULL,
    [LiftGateDestination]    BIT            NULL,
    [AppointmentDestination] DATETIME       NULL,
    [PalletRate]             DECIMAL (9, 2) NOT NULL,
    [FuelSurcharge]          DECIMAL (9, 2) NOT NULL,
    [AccessorialCharge]      DECIMAL (9, 2) NOT NULL,
    [InsuranceCharge]        DECIMAL (9, 2) NOT NULL,
    [TollCharge]             DECIMAL (9, 2) NOT NULL,
    [TotalCharge]            DECIMAL (9, 2) NOT NULL,
    [PickupID]               INT            NULL,
    [PickupDate]             DATETIME       NULL,
    [LastUpdated]            DATETIME       NOT NULL,
    [UserID]                 VARCHAR (50)   NOT NULL,
    CONSTRAINT [PK_ltlShipment] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[ltlShipper]...';


GO
CREATE TABLE [dbo].[ltlShipper] (
    [ID]              INT          IDENTITY (100000, 1) NOT NULL,
    [ClientID]        INT          NOT NULL,
    [Name]            VARCHAR (40) NOT NULL,
    [AddressLine1]    VARCHAR (40) NOT NULL,
    [AddressLine2]    VARCHAR (40) NULL,
    [City]            VARCHAR (40) NOT NULL,
    [State]           CHAR (2)     NOT NULL,
    [Zip]             CHAR (5)     NOT NULL,
    [Zip4]            CHAR (4)     NULL,
    [ContactName]     VARCHAR (40) NOT NULL,
    [ContactPhone]    VARCHAR (24) NOT NULL,
    [ContactEmail]    VARCHAR (50) NOT NULL,
    [WindowStartTime] DATETIME     NULL,
    [WindowEndTime]   DATETIME     NULL,
    [Status]          CHAR (1)     NOT NULL,
    [LastUpdated]     DATETIME     NOT NULL,
    [UserID]          VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ltlShipper] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Creating DF_ltlClient_ContactEmail...';


GO
ALTER TABLE [dbo].[ltlClient]
    ADD CONSTRAINT [DF_ltlClient_ContactEmail] DEFAULT ('') FOR [ContactEmail];


GO
PRINT N'Creating DF_ltlClient_ContactName...';


GO
ALTER TABLE [dbo].[ltlClient]
    ADD CONSTRAINT [DF_ltlClient_ContactName] DEFAULT ('') FOR [ContactName];


GO
PRINT N'Creating DF_ltlClient_ContactPhone...';


GO
ALTER TABLE [dbo].[ltlClient]
    ADD CONSTRAINT [DF_ltlClient_ContactPhone] DEFAULT ('') FOR [ContactPhone];


GO
PRINT N'Creating DF_ltlClient_LastUpdated...';


GO
ALTER TABLE [dbo].[ltlClient]
    ADD CONSTRAINT [DF_ltlClient_LastUpdated] DEFAULT (getdate()) FOR [LastUpdated];


GO
PRINT N'Creating DF_ltlClient_Status...';


GO
ALTER TABLE [dbo].[ltlClient]
    ADD CONSTRAINT [DF_ltlClient_Status] DEFAULT ('I') FOR [Status];


GO
PRINT N'Creating DF_ltlClient_UserID...';


GO
ALTER TABLE [dbo].[ltlClient]
    ADD CONSTRAINT [DF_ltlClient_UserID] DEFAULT (suser_sname()) FOR [UserID];


GO
PRINT N'Creating DF_ltlConsignee_ContactEmail...';


GO
ALTER TABLE [dbo].[ltlConsignee]
    ADD CONSTRAINT [DF_ltlConsignee_ContactEmail] DEFAULT ('') FOR [ContactEmail];


GO
PRINT N'Creating DF_ltlConsignee_ContactName...';


GO
ALTER TABLE [dbo].[ltlConsignee]
    ADD CONSTRAINT [DF_ltlConsignee_ContactName] DEFAULT ('') FOR [ContactName];


GO
PRINT N'Creating DF_ltlConsignee_ContactPhone...';


GO
ALTER TABLE [dbo].[ltlConsignee]
    ADD CONSTRAINT [DF_ltlConsignee_ContactPhone] DEFAULT ('') FOR [ContactPhone];


GO
PRINT N'Creating DF_ltlConsignee_LastUpdated...';


GO
ALTER TABLE [dbo].[ltlConsignee]
    ADD CONSTRAINT [DF_ltlConsignee_LastUpdated] DEFAULT (getdate()) FOR [LastUpdated];


GO
PRINT N'Creating DF_ltlConsignee_Status...';


GO
ALTER TABLE [dbo].[ltlConsignee]
    ADD CONSTRAINT [DF_ltlConsignee_Status] DEFAULT ('A') FOR [Status];


GO
PRINT N'Creating DF_ltlConsignee_UserID...';


GO
ALTER TABLE [dbo].[ltlConsignee]
    ADD CONSTRAINT [DF_ltlConsignee_UserID] DEFAULT (suser_sname()) FOR [UserID];


GO
PRINT N'Creating DF_ltlPallet_NMFCClass...';


GO
ALTER TABLE [dbo].[ltlPallet]
    ADD CONSTRAINT [DF_ltlPallet_NMFCClass] DEFAULT ('FAK') FOR [NMFCClass];


GO
PRINT N'Creating DF_ltlQuoteLog_Created...';


GO
ALTER TABLE [dbo].[ltlQuoteLog]
    ADD CONSTRAINT [DF_ltlQuoteLog_Created] DEFAULT (getdate()) FOR [Created];


GO
PRINT N'Creating DF_ltlShipment_LastUpdated...';


GO
ALTER TABLE [dbo].[ltlShipment]
    ADD CONSTRAINT [DF_ltlShipment_LastUpdated] DEFAULT (getdate()) FOR [LastUpdated];


GO
PRINT N'Creating DF_ltlShipment_UserID...';


GO
ALTER TABLE [dbo].[ltlShipment]
    ADD CONSTRAINT [DF_ltlShipment_UserID] DEFAULT (suser_sname()) FOR [UserID];


GO
PRINT N'Creating DF_ltlShipper_ContactEmail...';


GO
ALTER TABLE [dbo].[ltlShipper]
    ADD CONSTRAINT [DF_ltlShipper_ContactEmail] DEFAULT ('') FOR [ContactEmail];


GO
PRINT N'Creating DF_ltlShipper_ContactName...';


GO
ALTER TABLE [dbo].[ltlShipper]
    ADD CONSTRAINT [DF_ltlShipper_ContactName] DEFAULT ('') FOR [ContactName];


GO
PRINT N'Creating DF_ltlShipper_ContactPhone...';


GO
ALTER TABLE [dbo].[ltlShipper]
    ADD CONSTRAINT [DF_ltlShipper_ContactPhone] DEFAULT ('') FOR [ContactPhone];


GO
PRINT N'Creating DF_ltlShipper_LastUpdated...';


GO
ALTER TABLE [dbo].[ltlShipper]
    ADD CONSTRAINT [DF_ltlShipper_LastUpdated] DEFAULT (getdate()) FOR [LastUpdated];


GO
PRINT N'Creating DF_ltlShipper_Status...';


GO
ALTER TABLE [dbo].[ltlShipper]
    ADD CONSTRAINT [DF_ltlShipper_Status] DEFAULT ('A') FOR [Status];


GO
PRINT N'Creating DF_ltlShipper_UserID...';


GO
ALTER TABLE [dbo].[ltlShipper]
    ADD CONSTRAINT [DF_ltlShipper_UserID] DEFAULT (suser_sname()) FOR [UserID];


GO
PRINT N'Creating FK_ltlConsignee_ltlClient...';


GO
ALTER TABLE [dbo].[ltlConsignee] WITH NOCHECK
    ADD CONSTRAINT [FK_ltlConsignee_ltlClient] FOREIGN KEY ([ClientID]) REFERENCES [dbo].[ltlClient] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_ltlPallet_ltlShipment...';


GO
ALTER TABLE [dbo].[ltlPallet] WITH NOCHECK
    ADD CONSTRAINT [FK_ltlPallet_ltlShipment] FOREIGN KEY ([ShipmentID]) REFERENCES [dbo].[ltlShipment] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_ltlShipment_ltlClient...';


GO
ALTER TABLE [dbo].[ltlShipment] WITH NOCHECK
    ADD CONSTRAINT [FK_ltlShipment_ltlClient] FOREIGN KEY ([ClientID]) REFERENCES [dbo].[ltlClient] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_ltlShipment_ltlConsignee...';


GO
ALTER TABLE [dbo].[ltlShipment] WITH NOCHECK
    ADD CONSTRAINT [FK_ltlShipment_ltlConsignee] FOREIGN KEY ([ConsigneeID]) REFERENCES [dbo].[ltlConsignee] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_ltlShipment_ltlShipper...';


GO
ALTER TABLE [dbo].[ltlShipment] WITH NOCHECK
    ADD CONSTRAINT [FK_ltlShipment_ltlShipper] FOREIGN KEY ([ShipperID]) REFERENCES [dbo].[ltlShipper] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_ltlShipper_ltlClient...';


GO
ALTER TABLE [dbo].[ltlShipper] WITH NOCHECK
    ADD CONSTRAINT [FK_ltlShipper_ltlClient] FOREIGN KEY ([ClientID]) REFERENCES [dbo].[ltlClient] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
-- Refactoring step to update target server with deployed transaction logs
CREATE TABLE  [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
GO
sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
GO

GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[ltlConsignee] WITH CHECK CHECK CONSTRAINT [FK_ltlConsignee_ltlClient];

ALTER TABLE [dbo].[ltlPallet] WITH CHECK CHECK CONSTRAINT [FK_ltlPallet_ltlShipment];

ALTER TABLE [dbo].[ltlShipment] WITH CHECK CHECK CONSTRAINT [FK_ltlShipment_ltlClient];

ALTER TABLE [dbo].[ltlShipment] WITH CHECK CHECK CONSTRAINT [FK_ltlShipment_ltlConsignee];

ALTER TABLE [dbo].[ltlShipment] WITH CHECK CHECK CONSTRAINT [FK_ltlShipment_ltlShipper];

ALTER TABLE [dbo].[ltlShipper] WITH CHECK CHECK CONSTRAINT [FK_ltlShipper_ltlClient];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        DECLARE @VarDecimalSupported AS BIT;
        SELECT @VarDecimalSupported = 0;
        IF ((ServerProperty(N'EngineEdition') = 3)
            AND (((@@microsoftversion / power(2, 24) = 9)
                  AND (@@microsoftversion & 0xffff >= 3024))
                 OR ((@@microsoftversion / power(2, 24) = 10)
                     AND (@@microsoftversion & 0xffff >= 1600))))
            SELECT @VarDecimalSupported = 1;
        IF (@VarDecimalSupported > 0)
            BEGIN
                EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
            END
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET MULTI_USER 
    WITH ROLLBACK IMMEDIATE;


GO
