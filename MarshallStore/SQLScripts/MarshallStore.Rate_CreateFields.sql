USE [fiyistack_MarshallStore]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 31/07/2023 14:25:34

ALTER TABLE [dbo].[MarshallStore.Rate] ADD [RateId] INT IDENTITY(1,1) NOT NULL
ALTER TABLE [dbo].[MarshallStore.Rate] ADD [Active] TINYINT NOT NULL
ALTER TABLE [dbo].[MarshallStore.Rate] ADD [DateTimeCreation] DATETIME NOT NULL
ALTER TABLE [dbo].[MarshallStore.Rate] ADD [DateTimeLastModification] DATETIME NOT NULL
ALTER TABLE [dbo].[MarshallStore.Rate] ADD [UserCreationId] INT NOT NULL
ALTER TABLE [dbo].[MarshallStore.Rate] ADD [UserLastModificationId] INT NOT NULL
ALTER TABLE [dbo].[MarshallStore.Rate] ADD [ProductId] INT NOT NULL
ALTER TABLE [dbo].[MarshallStore.Rate] ADD [Score] INT NOT NULL
