USE [fiyistack_MarshallStore]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 31/07/2023 14:25:28

CREATE TABLE [dbo].[MarshallStore.Rate] (
    [RateId] [int] IDENTITY(1,1) NOT NULL,
    CONSTRAINT [PK_MarshallStoreRate] PRIMARY KEY CLUSTERED ([RateId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY])
ON[PRIMARY]