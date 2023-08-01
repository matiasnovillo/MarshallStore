CREATE PROCEDURE [dbo].[MarshallStore.PurchaseProduct.SelectAll]

AS

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2023
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

/*
 * Execute this stored procedure with the next script as example
 *
EXEC [dbo].[MarshallStore.PurchaseProduct.SelectAll]
 *
 */

--Last modification on: 31/07/2023 14:25:24

SET DATEFORMAT DMY

SELECT
    [MarshallStore.PurchaseProduct].[PurchaseProductId],
    [MarshallStore.PurchaseProduct].[Active],
    [MarshallStore.PurchaseProduct].[DateTimeCreation],
    [MarshallStore.PurchaseProduct].[DateTimeLastModification],
    [MarshallStore.PurchaseProduct].[UserCreationId],
    [MarshallStore.PurchaseProduct].[UserLastModificationId],
    [MarshallStore.PurchaseProduct].[PurchaseId],
    [MarshallStore.PurchaseProduct].[ProductId]
FROM 
    [MarshallStore.PurchaseProduct]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [MarshallStore.PurchaseProduct].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [MarshallStore.PurchaseProduct].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
ORDER BY 
    [MarshallStore.PurchaseProduct].[PurchaseProductId]