USE [ReportDB]
GO

/****** Object: Table [dbo].[FilterItem] Script Date: 04/10/2017 12:32:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Update  [dbo].[FilterItem] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [TemplateId]       INT            NOT NULL,
    [Name]             NVARCHAR (50)  NULL,
    [Priority]         INT            NULL,
	[Operation]         NVARCHAR (50)  NULL,
    [Value]            NVARCHAR (250) NULL,
    [LastModifieddate] DATETIME       NULL,
    [LastModifiedUser] NVARCHAR (50)  NULL,
    [CreatedDate]      DATETIME       NULL
);


