USE [fiyistack_MarshallStore]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 31/07/2023 14:32:15

ALTER TABLE [dbo].[MarshallStore.Product] ADD [ProductId] INT IDENTITY(1,1) NOT NULL
ALTER TABLE [dbo].[MarshallStore.Product] ADD [Active] TINYINT NOT NULL
ALTER TABLE [dbo].[MarshallStore.Product] ADD [DateTimeCreation] DATETIME NOT NULL
ALTER TABLE [dbo].[MarshallStore.Product] ADD [DateTimeLastModification] DATETIME NOT NULL
ALTER TABLE [dbo].[MarshallStore.Product] ADD [UserCreationId] INT NOT NULL
ALTER TABLE [dbo].[MarshallStore.Product] ADD [UserLastModificationId] INT NOT NULL
ALTER TABLE [dbo].[MarshallStore.Product] ADD [ProductCategoryId] INT NOT NULL
ALTER TABLE [dbo].[MarshallStore.Product] ADD [Producer] VARCHAR(300) NOT NULL
ALTER TABLE [dbo].[MarshallStore.Product] ADD [Model] VARCHAR(300) NOT NULL
ALTER TABLE [dbo].[MarshallStore.Product] ADD [Price] NUMERIC(24,6) NOT NULL
ALTER TABLE [dbo].[MarshallStore.Product] ADD [Description] VARCHAR(8000) NOT NULL
ALTER TABLE [dbo].[MarshallStore.Product] ADD [Image1] VARCHAR(8000) NOT NULL
ALTER TABLE [dbo].[MarshallStore.Product] ADD [Image2] VARCHAR(8000) NOT NULL
ALTER TABLE [dbo].[MarshallStore.Product] ADD [Image3] VARCHAR(8000) NOT NULL
