CREATE PROCEDURE [dbo].[MarshallStore.ProductCategory.SelectAllPaged]
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
EXEC [dbo].[MarshallStore.ProductCategory.SelectAllPaged]
    
    @QueryString = N'',
    @ActualPageNumber = 1,
    @RowsPerPage = 10,
    @SorterColumn = N'ProductCategoryId',
    @SortToggler = 0,
    @TotalRows = @TotalRows OUTPUT

SELECT @TotalRows AS N'@TotalRows'
*/

--Last modification on: 31/07/2023 14:25:12

SET DATEFORMAT DMY
SET NOCOUNT ON

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
    1=1
    AND (@QueryString = '' 
        OR ([MarshallStore.ProductCategory].[ProductCategoryId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.ProductCategory].[Active] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.ProductCategory].[DateTimeCreation] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.ProductCategory].[DateTimeLastModification] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.ProductCategory].[UserCreationId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.ProductCategory].[UserLastModificationId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.ProductCategory].[Name] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.ProductCategory].[Description] LIKE  '%' + @QueryString + '%')

    )
ORDER BY 
    CASE WHEN (@SorterColumn = 'ProductCategoryId' AND @SortToggler = 0) THEN [MarshallStore.ProductCategory].[ProductCategoryId] END ASC,
    CASE WHEN (@SorterColumn = 'ProductCategoryId' AND @SortToggler = 1) THEN [MarshallStore.ProductCategory].[ProductCategoryId] END DESC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 0) THEN [MarshallStore.ProductCategory].[Active] END ASC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 1) THEN [MarshallStore.ProductCategory].[Active] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 0) THEN [MarshallStore.ProductCategory].[DateTimeCreation] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 1) THEN [MarshallStore.ProductCategory].[DateTimeCreation] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 0) THEN [MarshallStore.ProductCategory].[DateTimeLastModification] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 1) THEN [MarshallStore.ProductCategory].[DateTimeLastModification] END DESC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 0) THEN [MarshallStore.ProductCategory].[UserCreationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 1) THEN [MarshallStore.ProductCategory].[UserCreationId] END DESC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 0) THEN [MarshallStore.ProductCategory].[UserLastModificationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 1) THEN [MarshallStore.ProductCategory].[UserLastModificationId] END DESC,
    CASE WHEN (@SorterColumn = 'Name' AND @SortToggler = 0) THEN [MarshallStore.ProductCategory].[Name] END ASC,
    CASE WHEN (@SorterColumn = 'Name' AND @SortToggler = 1) THEN [MarshallStore.ProductCategory].[Name] END DESC

OFFSET (@ActualPageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY
SELECT @TotalRows = COUNT(*) FROM [MarshallStore.ProductCategory]