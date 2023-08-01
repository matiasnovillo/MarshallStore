USE [fiyistack_MarshallStore]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 31/07/2023 14:25:17

ALTER TABLE [dbo].[MarshallStore.Purchase] ADD [PurchaseId] INT IDENTITY(1,1) NOT NULL
ALTER TABLE [dbo].[MarshallStore.Purchase] ADD [Active] TINYINT NOT NULL
ALTER TABLE [dbo].[MarshallStore.Purchase] ADD [DateTimeCreation] DATETIME NOT NULL
ALTER TABLE [dbo].[MarshallStore.Purchase] ADD [DateTimeLastModification] DATETIME NOT NULL
ALTER TABLE [dbo].[MarshallStore.Purchase] ADD [UserCreationId] INT NOT NULL
ALTER TABLE [dbo].[MarshallStore.Purchase] ADD [UserLastModificationId] INT NOT NULL
ALTER TABLE [dbo].[MarshallStore.Purchase] ADD [Address] VARCHAR(8000) NOT NULL
ALTER TABLE [dbo].[MarshallStore.Purchase] ADD [FullPrice] NUMERIC(24,6) NOT NULL
