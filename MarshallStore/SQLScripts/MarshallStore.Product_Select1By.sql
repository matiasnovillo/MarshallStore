CREATE PROCEDURE [dbo].[MarshallStore.Product.Select1ByProductId]
(
    @ProductId INT
)

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
EXEC [dbo].[Product.Select1ByProductId]
    @ProductId = 1
 *
 */

--Last modification on: 31/07/2023 14:32:14

SET DATEFORMAT DMY

SELECT
    [MarshallStore.Product].[ProductId],
    [MarshallStore.Product].[Active],
    [MarshallStore.Product].[DateTimeCreation],
    [MarshallStore.Product].[DateTimeLastModification],
    [MarshallStore.Product].[UserCreationId],
    [MarshallStore.Product].[UserLastModificationId],
    [MarshallStore.Product].[ProductCategoryId],
    [MarshallStore.Product].[Producer],
    [MarshallStore.Product].[Model],
    [MarshallStore.Product].[Price],
    [MarshallStore.Product].[Description],
    [MarshallStore.Product].[Image1],
    [MarshallStore.Product].[Image2],
    [MarshallStore.Product].[Image3]
FROM 
    [MarshallStore.Product]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [MarshallStore.Product].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [MarshallStore.Product].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
WHERE 
    1 = 1
    AND [MarshallStore.Product].[ProductId] = @ProductId
ORDER BY 
    [MarshallStore.Product].[ProductId]