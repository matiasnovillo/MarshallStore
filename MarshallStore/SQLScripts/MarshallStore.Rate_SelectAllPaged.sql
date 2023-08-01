CREATE PROCEDURE [dbo].[MarshallStore.Rate.SelectAllPaged]
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
EXEC [dbo].[MarshallStore.Rate.SelectAllPaged]
    
    @QueryString = N'',
    @ActualPageNumber = 1,
    @RowsPerPage = 10,
    @SorterColumn = N'RateId',
    @SortToggler = 0,
    @TotalRows = @TotalRows OUTPUT

SELECT @TotalRows AS N'@TotalRows'
*/

--Last modification on: 31/07/2023 14:25:33

SET DATEFORMAT DMY
SET NOCOUNT ON

SELECT
    [MarshallStore.Rate].[RateId],
    [MarshallStore.Rate].[Active],
    [MarshallStore.Rate].[DateTimeCreation],
    [MarshallStore.Rate].[DateTimeLastModification],
    [MarshallStore.Rate].[UserCreationId],
    [MarshallStore.Rate].[UserLastModificationId],
    [MarshallStore.Rate].[ProductId],
    [MarshallStore.Rate].[Score]
FROM 
    [MarshallStore.Rate]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [MarshallStore.Rate].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [MarshallStore.Rate].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
WHERE
    1=1
    AND (@QueryString = '' 
        OR ([MarshallStore.Rate].[RateId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Rate].[Active] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Rate].[DateTimeCreation] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Rate].[DateTimeLastModification] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Rate].[UserCreationId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Rate].[UserLastModificationId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Rate].[ProductId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Rate].[Score] LIKE  '%' + @QueryString + '%')

    )
ORDER BY 
    CASE WHEN (@SorterColumn = 'RateId' AND @SortToggler = 0) THEN [MarshallStore.Rate].[RateId] END ASC,
    CASE WHEN (@SorterColumn = 'RateId' AND @SortToggler = 1) THEN [MarshallStore.Rate].[RateId] END DESC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 0) THEN [MarshallStore.Rate].[Active] END ASC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 1) THEN [MarshallStore.Rate].[Active] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 0) THEN [MarshallStore.Rate].[DateTimeCreation] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 1) THEN [MarshallStore.Rate].[DateTimeCreation] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 0) THEN [MarshallStore.Rate].[DateTimeLastModification] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 1) THEN [MarshallStore.Rate].[DateTimeLastModification] END DESC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 0) THEN [MarshallStore.Rate].[UserCreationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 1) THEN [MarshallStore.Rate].[UserCreationId] END DESC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 0) THEN [MarshallStore.Rate].[UserLastModificationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 1) THEN [MarshallStore.Rate].[UserLastModificationId] END DESC,
    CASE WHEN (@SorterColumn = 'ProductId' AND @SortToggler = 0) THEN [MarshallStore.Rate].[ProductId] END ASC,
    CASE WHEN (@SorterColumn = 'ProductId' AND @SortToggler = 1) THEN [MarshallStore.Rate].[ProductId] END DESC,
    CASE WHEN (@SorterColumn = 'Score' AND @SortToggler = 0) THEN [MarshallStore.Rate].[Score] END ASC,
    CASE WHEN (@SorterColumn = 'Score' AND @SortToggler = 1) THEN [MarshallStore.Rate].[Score] END DESC

OFFSET (@ActualPageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY
SELECT @TotalRows = COUNT(*) FROM [MarshallStore.Rate]