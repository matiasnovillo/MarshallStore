USE [fiyistack_MarshallStore]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 31/07/2023 14:25:10

CREATE TABLE [dbo].[MarshallStore.ProductCategory] (
    [ProductCategoryId] [int] IDENTITY(1,1) NOT NULL,
    CONSTRAINT [PK_MarshallStoreProductCategory] PRIMARY KEY CLUSTERED ([ProductCategoryId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY])
ON[PRIMARY]