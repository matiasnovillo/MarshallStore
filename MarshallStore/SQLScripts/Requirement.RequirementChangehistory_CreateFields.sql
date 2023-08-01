USE [fiyistack_MarshallStore]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 24/12/2022 6:48:12

ALTER TABLE [dbo].[Requirement.RequirementChangehistory] ADD [RequirementChangehistoryId] INT IDENTITY(1,1) NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementChangehistory] ADD [Active] TINYINT NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementChangehistory] ADD [DateTimeCreation] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementChangehistory] ADD [DateTimeLastModification] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementChangehistory] ADD [UserCreationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementChangehistory] ADD [UserLastModificationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementChangehistory] ADD [RequirementId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementChangehistory] ADD [RequirementStateId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementChangehistory] ADD [RequirementPriorityId] INT NOT NULL
