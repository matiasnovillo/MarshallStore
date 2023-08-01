USE [fiyistack_MarshallStore]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 24/12/2022 5:23:01

CREATE TABLE [dbo].[Requirement.RequirementState] (
    [RequirementStateId] [int] IDENTITY(1,1) NOT NULL,
    CONSTRAINT [PK_RequirementRequirementState] PRIMARY KEY CLUSTERED ([RequirementStateId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY])
ON[PRIMARY]