CREATE PROCEDURE [dbo].[MarshallStore.Purchase.SelectAll]

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
EXEC [dbo].[MarshallStore.Purchase.SelectAll]
 *
 */

--Last modification on: 31/07/2023 14:25:17

SET DATEFORMAT DMY

SELECT
    [MarshallStore.Purchase].[PurchaseId],
    [MarshallStore.Purchase].[Active],
    [MarshallStore.Purchase].[DateTimeCreation],
    [MarshallStore.Purchase].[DateTimeLastModification],
    [MarshallStore.Purchase].[UserCreationId],
    [MarshallStore.Purchase].[UserLastModificationId],
    [MarshallStore.Purchase].[Address],
    [MarshallStore.Purchase].[FullPrice]
FROM 
    [MarshallStore.Purchase]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [MarshallStore.Purchase].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [MarshallStore.Purchase].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
ORDER BY 
    [MarshallStore.Purchase].[PurchaseId]