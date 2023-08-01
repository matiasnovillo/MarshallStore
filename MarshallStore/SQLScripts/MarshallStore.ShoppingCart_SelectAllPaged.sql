CREATE PROCEDURE [dbo].[MarshallStore.ShoppingCart.SelectAllPaged]
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
EXEC [dbo].[MarshallStore.ShoppingCart.SelectAllPaged]
    
    @QueryString = N'',
    @ActualPageNumber = 1,
    @RowsPerPage = 10,
    @SorterColumn = N'ShoppingCartId',
    @SortToggler = 0,
    @TotalRows = @TotalRows OUTPUT

SELECT @TotalRows AS N'@TotalRows'
*/

--Last modification on: 31/07/2023 14:45:17

SET DATEFORMAT DMY
SET NOCOUNT ON

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
WHERE
    1=1
    AND (@QueryString = '' 
        OR ([MarshallStore.ShoppingCart].[ShoppingCartId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.ShoppingCart].[Active] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.ShoppingCart].[DateTimeCreation] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.ShoppingCart].[DateTimeLastModification] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.ShoppingCart].[UserCreationId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.ShoppingCart].[UserLastModificationId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.ShoppingCart].[ProductId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.ShoppingCart].[Counter] LIKE  '%' + @QueryString + '%')

    )
ORDER BY 
    CASE WHEN (@SorterColumn = 'ShoppingCartId' AND @SortToggler = 0) THEN [MarshallStore.ShoppingCart].[ShoppingCartId] END ASC,
    CASE WHEN (@SorterColumn = 'ShoppingCartId' AND @SortToggler = 1) THEN [MarshallStore.ShoppingCart].[ShoppingCartId] END DESC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 0) THEN [MarshallStore.ShoppingCart].[Active] END ASC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 1) THEN [MarshallStore.ShoppingCart].[Active] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 0) THEN [MarshallStore.ShoppingCart].[DateTimeCreation] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 1) THEN [MarshallStore.ShoppingCart].[DateTimeCreation] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 0) THEN [MarshallStore.ShoppingCart].[DateTimeLastModification] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 1) THEN [MarshallStore.ShoppingCart].[DateTimeLastModification] END DESC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 0) THEN [MarshallStore.ShoppingCart].[UserCreationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 1) THEN [MarshallStore.ShoppingCart].[UserCreationId] END DESC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 0) THEN [MarshallStore.ShoppingCart].[UserLastModificationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 1) THEN [MarshallStore.ShoppingCart].[UserLastModificationId] END DESC,
    CASE WHEN (@SorterColumn = 'ProductId' AND @SortToggler = 0) THEN [MarshallStore.ShoppingCart].[ProductId] END ASC,
    CASE WHEN (@SorterColumn = 'ProductId' AND @SortToggler = 1) THEN [MarshallStore.ShoppingCart].[ProductId] END DESC,
    CASE WHEN (@SorterColumn = 'Counter' AND @SortToggler = 0) THEN [MarshallStore.ShoppingCart].[Counter] END ASC,
    CASE WHEN (@SorterColumn = 'Counter' AND @SortToggler = 1) THEN [MarshallStore.ShoppingCart].[Counter] END DESC

OFFSET (@ActualPageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY
SELECT @TotalRows = COUNT(*) FROM [MarshallStore.ShoppingCart]