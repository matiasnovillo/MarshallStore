USE [fiyistack_MarshallStore]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 31/07/2023 14:45:18

ALTER TABLE [dbo].[MarshallStore.ShoppingCart] ADD [ShoppingCartId] INT IDENTITY(1,1) NOT NULL
ALTER TABLE [dbo].[MarshallStore.ShoppingCart] ADD [Active] TINYINT NOT NULL
ALTER TABLE [dbo].[MarshallStore.ShoppingCart] ADD [DateTimeCreation] DATETIME NOT NULL
ALTER TABLE [dbo].[MarshallStore.ShoppingCart] ADD [DateTimeLastModification] DATETIME NOT NULL
ALTER TABLE [dbo].[MarshallStore.ShoppingCart] ADD [UserCreationId] INT NOT NULL
ALTER TABLE [dbo].[MarshallStore.ShoppingCart] ADD [UserLastModificationId] INT NOT NULL
ALTER TABLE [dbo].[MarshallStore.ShoppingCart] ADD [ProductId] INT NOT NULL
ALTER TABLE [dbo].[MarshallStore.ShoppingCart] ADD [Counter] INT NOT NULL
