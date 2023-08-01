CREATE PROCEDURE [dbo].[MarshallStore.ProductCategory.Select1ByProductCategoryId]
(
    @ProductCategoryId INT
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
EXEC [dbo].[ProductCategory.Select1ByProductCategoryId]
    @ProductCategoryId = 1
 *
 */

--Last modification on: 31/07/2023 14:25:12

SET DATEFORMAT DMY

SELECT
    [MarshallStore.ProductCategory].[ProductCategoryId],
    [MarshallStore.ProductCategory].[Active],
    [MarshallStore.ProductCategory].[DateTimeCreation],
    [MarshallStore.ProductCategory].[DateTimeLastModification],
    [MarshallStore.ProductCategory].[UserCreationId],
    [MarshallStore.ProductCategory].[UserLastModificationId],
    [MarshallStore.ProductCategory].[Name],
    [MarshallStore.ProductCategory].[Description]
FROM 
    [MarshallStore.ProductCategory]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [MarshallStore.ProductCategory].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [MarshallStore.ProductCategory].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
WHERE 
    1 = 1
    AND [MarshallStore.ProductCategory].[ProductCategoryId] = @ProductCategoryId
ORDER BY 
    [MarshallStore.ProductCategory].[ProductCategoryId]