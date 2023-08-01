USE [fiyistack_MarshallStore]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 31/07/2023 14:25:12

ALTER TABLE [dbo].[MarshallStore.ProductCategory] ADD [ProductCategoryId] INT IDENTITY(1,1) NOT NULL
ALTER TABLE [dbo].[MarshallStore.ProductCategory] ADD [Active] TINYINT NOT NULL
ALTER TABLE [dbo].[MarshallStore.ProductCategory] ADD [DateTimeCreation] DATETIME NOT NULL
ALTER TABLE [dbo].[MarshallStore.ProductCategory] ADD [DateTimeLastModification] DATETIME NOT NULL
ALTER TABLE [dbo].[MarshallStore.ProductCategory] ADD [UserCreationId] INT NOT NULL
ALTER TABLE [dbo].[MarshallStore.ProductCategory] ADD [UserLastModificationId] INT NOT NULL
ALTER TABLE [dbo].[MarshallStore.ProductCategory] ADD [Name] VARCHAR(300) NOT NULL
ALTER TABLE [dbo].[MarshallStore.ProductCategory] ADD [Description] VARCHAR(8000) NOT NULL
