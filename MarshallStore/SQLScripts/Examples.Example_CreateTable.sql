USE [fiyistack_MarshallStore]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 31/01/2023 7:54:00

CREATE TABLE [dbo].[Examples.Example] (
    [ExampleId] [int] IDENTITY(1,1) NOT NULL,
    CONSTRAINT [PK_ExamplesExample] PRIMARY KEY CLUSTERED ([ExampleId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY])
ON[PRIMARY]