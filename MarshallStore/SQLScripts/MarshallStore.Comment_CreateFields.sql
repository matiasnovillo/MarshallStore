USE [fiyistack_MarshallStore]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

--Last modification on: 31/07/2023 14:24:26

ALTER TABLE [dbo].[MarshallStore.Comment] ADD [CommentId] INT IDENTITY(1,1) NOT NULL
ALTER TABLE [dbo].[MarshallStore.Comment] ADD [Active] TINYINT NOT NULL
ALTER TABLE [dbo].[MarshallStore.Comment] ADD [DateTimeCreation] DATETIME NOT NULL
ALTER TABLE [dbo].[MarshallStore.Comment] ADD [DateTimeLastModification] DATETIME NOT NULL
ALTER TABLE [dbo].[MarshallStore.Comment] ADD [UserCreationId] INT NOT NULL
ALTER TABLE [dbo].[MarshallStore.Comment] ADD [UserLastModificationId] INT NOT NULL
ALTER TABLE [dbo].[MarshallStore.Comment] ADD [ProductId] INT NOT NULL
ALTER TABLE [dbo].[MarshallStore.Comment] ADD [Text] VARCHAR(8000) NOT NULL
