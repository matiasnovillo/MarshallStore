USE [fiyistack_MarshallStore]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 24/12/2022 6:47:08

ALTER TABLE [dbo].[Requirement.RequirementPriority] ADD [RequirementPriorityId] INT IDENTITY(1,1) NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementPriority] ADD [Active] TINYINT NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementPriority] ADD [DateTimeCreation] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementPriority] ADD [DateTimeLastModification] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementPriority] ADD [UserCreationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementPriority] ADD [UserLastModificationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementPriority] ADD [Name] VARCHAR(100) NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementPriority] ADD [Description] VARCHAR(2000) NOT NULL
