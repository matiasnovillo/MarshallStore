USE [fiyistack_MarshallStore]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 24/12/2022 6:47:58

ALTER TABLE [dbo].[Requirement.RequirementNote] ADD [RequirementNoteId] INT IDENTITY(1,1) NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementNote] ADD [Active] TINYINT NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementNote] ADD [DateTimeCreation] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementNote] ADD [DateTimeLastModification] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementNote] ADD [UserCreationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementNote] ADD [UserLastModificationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementNote] ADD [Title] VARCHAR(100) NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementNote] ADD [Body] VARCHAR(8000) NOT NULL
