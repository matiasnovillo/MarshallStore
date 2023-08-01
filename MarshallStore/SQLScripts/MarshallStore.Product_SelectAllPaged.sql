CREATE PROCEDURE [dbo].[MarshallStore.Product.SelectAllPaged]
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
EXEC [dbo].[MarshallStore.Product.SelectAllPaged]
    
    @QueryString = N'',
    @ActualPageNumber = 1,
    @RowsPerPage = 10,
    @SorterColumn = N'ProductId',
    @SortToggler = 0,
    @TotalRows = @TotalRows OUTPUT

SELECT @TotalRows AS N'@TotalRows'
*/

--Last modification on: 31/07/2023 14:32:15

SET DATEFORMAT DMY
SET NOCOUNT ON

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
    1=1
    AND (@QueryString = '' 
        OR ([MarshallStore.Product].[ProductId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Product].[Active] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Product].[DateTimeCreation] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Product].[DateTimeLastModification] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Product].[UserCreationId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Product].[UserLastModificationId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Product].[ProductCategoryId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Product].[Producer] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Product].[Model] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Product].[Price] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Product].[Description] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Product].[Image1] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Product].[Image2] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Product].[Image3] LIKE  '%' + @QueryString + '%')

    )
ORDER BY 
    CASE WHEN (@SorterColumn = 'ProductId' AND @SortToggler = 0) THEN [MarshallStore.Product].[ProductId] END ASC,
    CASE WHEN (@SorterColumn = 'ProductId' AND @SortToggler = 1) THEN [MarshallStore.Product].[ProductId] END DESC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 0) THEN [MarshallStore.Product].[Active] END ASC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 1) THEN [MarshallStore.Product].[Active] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 0) THEN [MarshallStore.Product].[DateTimeCreation] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 1) THEN [MarshallStore.Product].[DateTimeCreation] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 0) THEN [MarshallStore.Product].[DateTimeLastModification] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 1) THEN [MarshallStore.Product].[DateTimeLastModification] END DESC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 0) THEN [MarshallStore.Product].[UserCreationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 1) THEN [MarshallStore.Product].[UserCreationId] END DESC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 0) THEN [MarshallStore.Product].[UserLastModificationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 1) THEN [MarshallStore.Product].[UserLastModificationId] END DESC,
    CASE WHEN (@SorterColumn = 'ProductCategoryId' AND @SortToggler = 0) THEN [MarshallStore.Product].[ProductCategoryId] END ASC,
    CASE WHEN (@SorterColumn = 'ProductCategoryId' AND @SortToggler = 1) THEN [MarshallStore.Product].[ProductCategoryId] END DESC,
    CASE WHEN (@SorterColumn = 'Producer' AND @SortToggler = 0) THEN [MarshallStore.Product].[Producer] END ASC,
    CASE WHEN (@SorterColumn = 'Producer' AND @SortToggler = 1) THEN [MarshallStore.Product].[Producer] END DESC,
    CASE WHEN (@SorterColumn = 'Model' AND @SortToggler = 0) THEN [MarshallStore.Product].[Model] END ASC,
    CASE WHEN (@SorterColumn = 'Model' AND @SortToggler = 1) THEN [MarshallStore.Product].[Model] END DESC,
    CASE WHEN (@SorterColumn = 'Price' AND @SortToggler = 0) THEN [MarshallStore.Product].[Price] END ASC,
    CASE WHEN (@SorterColumn = 'Price' AND @SortToggler = 1) THEN [MarshallStore.Product].[Price] END DESC,
    CASE WHEN (@SorterColumn = 'Image1' AND @SortToggler = 0) THEN [MarshallStore.Product].[Image1] END ASC,
    CASE WHEN (@SorterColumn = 'Image1' AND @SortToggler = 1) THEN [MarshallStore.Product].[Image1] END DESC,
    CASE WHEN (@SorterColumn = 'Image2' AND @SortToggler = 0) THEN [MarshallStore.Product].[Image2] END ASC,
    CASE WHEN (@SorterColumn = 'Image2' AND @SortToggler = 1) THEN [MarshallStore.Product].[Image2] END DESC,
    CASE WHEN (@SorterColumn = 'Image3' AND @SortToggler = 0) THEN [MarshallStore.Product].[Image3] END ASC,
    CASE WHEN (@SorterColumn = 'Image3' AND @SortToggler = 1) THEN [MarshallStore.Product].[Image3] END DESC

OFFSET (@ActualPageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY
SELECT @TotalRows = COUNT(*) FROM [MarshallStore.Product]