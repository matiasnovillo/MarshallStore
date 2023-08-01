USE [fiyistack_MarshallStore]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 27/12/2022 20:52:58

ALTER TABLE [dbo].[Requirement.Requirement] ADD [RequirementId] INT IDENTITY(1,1) NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [Active] TINYINT NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [DateTimeCreation] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [DateTimeLastModification] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [UserCreationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [UserLastModificationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [Title] VARCHAR(100) NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [Body] VARCHAR(8000) NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [RequirementStateId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [RequirementPriorityId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [UserEmployeeId] INT NOT NULL
 [dbo].[Requirement.Requirement] ADD [RequirementPriorityId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.Requirement] ADD [UserProgrammerId] INT NOT NULL
