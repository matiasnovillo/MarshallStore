CREATE PROCEDURE [dbo].[MarshallStore.PurchaseProduct.SelectAllPaged]
(
    @QueryString VARCHAR(100),
    @ActualPageNumber INT,
    @RowsPerPage INT,
    @SorterColumn VARCHAR(100),
    @SortToggler TINYINT,
    @TotalRows INT OUTPUT
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

/*Execute this stored procedure with the next script as example

DECLARE	@TotalRows int
EXEC [dbo].[MarshallStore.PurchaseProduct.SelectAllPaged]
    
    @QueryString = N'',
    @ActualPageNumber = 1,
    @RowsPerPage = 10,
    @SorterColumn = N'PurchaseProductId',
    @SortToggler = 0,
    @TotalRows = @TotalRows OUTPUT

SELECT @TotalRows AS N'@TotalRows'
*/

--Last modification on: 31/07/2023 14:25:24

SET DATEFORMAT DMY
SET NOCOUNT ON

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
WHERE
    1=1
    AND (@QueryString = '' 
        OR ([MarshallStore.PurchaseProduct].[PurchaseProductId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.PurchaseProduct].[Active] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.PurchaseProduct].[DateTimeCreation] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.PurchaseProduct].[DateTimeLastModification] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.PurchaseProduct].[UserCreationId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.PurchaseProduct].[UserLastModificationId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.PurchaseProduct].[PurchaseId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.PurchaseProduct].[ProductId] LIKE  '%' + @QueryString + '%')

    )
ORDER BY 
    CASE WHEN (@SorterColumn = 'PurchaseProductId' AND @SortToggler = 0) THEN [MarshallStore.PurchaseProduct].[PurchaseProductId] END ASC,
    CASE WHEN (@SorterColumn = 'PurchaseProductId' AND @SortToggler = 1) THEN [MarshallStore.PurchaseProduct].[PurchaseProductId] END DESC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 0) THEN [MarshallStore.PurchaseProduct].[Active] END ASC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 1) THEN [MarshallStore.PurchaseProduct].[Active] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 0) THEN [MarshallStore.PurchaseProduct].[DateTimeCreation] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 1) THEN [MarshallStore.PurchaseProduct].[DateTimeCreation] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 0) THEN [MarshallStore.PurchaseProduct].[DateTimeLastModification] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 1) THEN [MarshallStore.PurchaseProduct].[DateTimeLastModification] END DESC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 0) THEN [MarshallStore.PurchaseProduct].[UserCreationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 1) THEN [MarshallStore.PurchaseProduct].[UserCreationId] END DESC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 0) THEN [MarshallStore.PurchaseProduct].[UserLastModificationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 1) THEN [MarshallStore.PurchaseProduct].[UserLastModificationId] END DESC,
    CASE WHEN (@SorterColumn = 'PurchaseId' AND @SortToggler = 0) THEN [MarshallStore.PurchaseProduct].[PurchaseId] END ASC,
    CASE WHEN (@SorterColumn = 'PurchaseId' AND @SortToggler = 1) THEN [MarshallStore.PurchaseProduct].[PurchaseId] END DESC,
    CASE WHEN (@SorterColumn = 'ProductId' AND @SortToggler = 0) THEN [MarshallStore.PurchaseProduct].[ProductId] END ASC,
    CASE WHEN (@SorterColumn = 'ProductId' AND @SortToggler = 1) THEN [MarshallStore.PurchaseProduct].[ProductId] END DESC

OFFSET (@ActualPageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY
SELECT @TotalRows = COUNT(*) FROM [MarshallStore.PurchaseProduct]