CREATE PROCEDURE [dbo].[MarshallStore.ShoppingCart.SelectAll]

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
EXEC [dbo].[MarshallStore.ShoppingCart.SelectAll]
 *
 */

--Last modification on: 31/07/2023 14:45:17

SET DATEFORMAT DMY

SELECT
    [MarshallStore.ShoppingCart].[ShoppingCartId],
    [MarshallStore.ShoppingCart].[Active],
    [MarshallStore.ShoppingCart].[DateTimeCreation],
    [MarshallStore.ShoppingCart].[DateTimeLastModification],
    [MarshallStore.ShoppingCart].[UserCreationId],
    [MarshallStore.ShoppingCart].[UserLastModificationId],
    [MarshallStore.ShoppingCart].[ProductId],
    [MarshallStore.ShoppingCart].[Counter]
FROM 
    [MarshallStore.ShoppingCart]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [MarshallStore.ShoppingCart].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [MarshallStore.ShoppingCart].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
ORDER BY 
    [MarshallStore.ShoppingCart].[ShoppingCartId]