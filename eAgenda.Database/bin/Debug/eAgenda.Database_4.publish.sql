﻿/*
Deployment script for Agenda

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Agenda"
:setvar DefaultFilePrefix "Agenda"
:setvar DefaultDataPath "C:\Users\odair\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSqlLocalDB\"
:setvar DefaultLogPath "C:\Users\odair\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSqlLocalDB\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Altering Table [dbo].[TBCompromisso]...';


GO
ALTER TABLE [dbo].[TBCompromisso] ALTER COLUMN [HoraFim] TIME (7) NOT NULL;

ALTER TABLE [dbo].[TBCompromisso] ALTER COLUMN [HoraInicio] TIME (7) NOT NULL;


GO
PRINT N'Update complete.';


GO