USE [fiyistack_MarshallStore]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 29/12/2022 10:16:50

ALTER TABLE [dbo].[Requirement.RequirementFile] ADD [RequirementFileId] INT IDENTITY(1,1) NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementFile] ADD [Active] TINYINT NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementFile] ADD [DateTimeCreation] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementFile] ADD [DateTimeLastModification] DATETIME NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementFile] ADD [UserCreationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementFile] ADD [UserLastModificationId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementFile] ADD [RequirementId] INT NOT NULL
ALTER TABLE [dbo].[Requirement.RequirementFile] ADD [FilePath] VARCHAR(8000) NOT NULL
LTER TABLE [dbo].[Requirement.RequirementFile] ADD [FilePath] VARCHAR(8000) NOT NULL
