USE [fiyistack_MarshallStore]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 31/07/2023 14:25:24

ALTER TABLE [dbo].[MarshallStore.PurchaseProduct] ADD [PurchaseProductId] INT IDENTITY(1,1) NOT NULL
ALTER TABLE [dbo].[MarshallStore.PurchaseProduct] ADD [Active] TINYINT NOT NULL
ALTER TABLE [dbo].[MarshallStore.PurchaseProduct] ADD [DateTimeCreation] DATETIME NOT NULL
ALTER TABLE [dbo].[MarshallStore.PurchaseProduct] ADD [DateTimeLastModification] DATETIME NOT NULL
ALTER TABLE [dbo].[MarshallStore.PurchaseProduct] ADD [UserCreationId] INT NOT NULL
ALTER TABLE [dbo].[MarshallStore.PurchaseProduct] ADD [UserLastModificationId] INT NOT NULL
ALTER TABLE [dbo].[MarshallStore.PurchaseProduct] ADD [PurchaseId] INT NOT NULL
ALTER TABLE [dbo].[MarshallStore.PurchaseProduct] ADD [ProductId] INT NOT NULL
