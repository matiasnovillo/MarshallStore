USE [fiyistack_MarshallStore]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 24/12/2022 6:47:04

ALTER TABLE [dbo].[Requirement.RequirementState] ADD [RequirementStateId] INT IDENTITY(1,1) NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementState] ADD [Active] TINYINT NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementState] ADD [DateTimeCreation] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementState] ADD [DateTimeLastModification] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementState] ADD [UserCreationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementState] ADD [UserLastModificationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementState] ADD [Name] VARCHAR(100) NOT NULL
