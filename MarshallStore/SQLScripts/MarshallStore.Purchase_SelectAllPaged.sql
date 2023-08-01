CREATE PROCEDURE [dbo].[MarshallStore.Purchase.SelectAllPaged]
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
EXEC [dbo].[MarshallStore.Purchase.SelectAllPaged]
    
    @QueryString = N'',
    @ActualPageNumber = 1,
    @RowsPerPage = 10,
    @SorterColumn = N'PurchaseId',
    @SortToggler = 0,
    @TotalRows = @TotalRows OUTPUT

SELECT @TotalRows AS N'@TotalRows'
*/

--Last modification on: 31/07/2023 14:25:17

SET DATEFORMAT DMY
SET NOCOUNT ON

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
WHERE
    1=1
    AND (@QueryString = '' 
        OR ([MarshallStore.Purchase].[PurchaseId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Purchase].[Active] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Purchase].[DateTimeCreation] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Purchase].[DateTimeLastModification] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Purchase].[UserCreationId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Purchase].[UserLastModificationId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Purchase].[Address] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Purchase].[FullPrice] LIKE  '%' + @QueryString + '%')

    )
ORDER BY 
    CASE WHEN (@SorterColumn = 'PurchaseId' AND @SortToggler = 0) THEN [MarshallStore.Purchase].[PurchaseId] END ASC,
    CASE WHEN (@SorterColumn = 'PurchaseId' AND @SortToggler = 1) THEN [MarshallStore.Purchase].[PurchaseId] END DESC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 0) THEN [MarshallStore.Purchase].[Active] END ASC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 1) THEN [MarshallStore.Purchase].[Active] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 0) THEN [MarshallStore.Purchase].[DateTimeCreation] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 1) THEN [MarshallStore.Purchase].[DateTimeCreation] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 0) THEN [MarshallStore.Purchase].[DateTimeLastModification] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 1) THEN [MarshallStore.Purchase].[DateTimeLastModification] END DESC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 0) THEN [MarshallStore.Purchase].[UserCreationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 1) THEN [MarshallStore.Purchase].[UserCreationId] END DESC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 0) THEN [MarshallStore.Purchase].[UserLastModificationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 1) THEN [MarshallStore.Purchase].[UserLastModificationId] END DESC,
    CASE WHEN (@SorterColumn = 'FullPrice' AND @SortToggler = 0) THEN [MarshallStore.Purchase].[FullPrice] END ASC,
    CASE WHEN (@SorterColumn = 'FullPrice' AND @SortToggler = 1) THEN [MarshallStore.Purchase].[FullPrice] END DESC

OFFSET (@ActualPageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY
SELECT @TotalRows = COUNT(*) FROM [MarshallStore.Purchase]