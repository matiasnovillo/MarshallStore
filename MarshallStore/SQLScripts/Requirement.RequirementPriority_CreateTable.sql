USE [fiyistack_MarshallStore]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 24/12/2022 6:11:57

CREATE TABLE [dbo].[Requirement.RequirementPriority] (
    [RequirementPriorityId] [int] IDENTITY(1,1) NOT NULL,
    CONSTRAINT [PK_RequirementRequirementPriority] PRIMARY KEY CLUSTERED ([RequirementPriorityId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY])
ON[PRIMARY]