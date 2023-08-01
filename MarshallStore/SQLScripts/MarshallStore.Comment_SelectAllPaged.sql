CREATE PROCEDURE [dbo].[MarshallStore.Comment.SelectAllPaged]
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
EXEC [dbo].[MarshallStore.Comment.SelectAllPaged]
    
    @QueryString = N'',
    @ActualPageNumber = 1,
    @RowsPerPage = 10,
    @SorterColumn = N'CommentId',
    @SortToggler = 0,
    @TotalRows = @TotalRows OUTPUT

SELECT @TotalRows AS N'@TotalRows'
*/

--Last modification on: 31/07/2023 14:24:26

SET DATEFORMAT DMY
SET NOCOUNT ON

SELECT
    [MarshallStore.Comment].[CommentId],
    [MarshallStore.Comment].[Active],
    [MarshallStore.Comment].[DateTimeCreation],
    [MarshallStore.Comment].[DateTimeLastModification],
    [MarshallStore.Comment].[UserCreationId],
    [MarshallStore.Comment].[UserLastModificationId],
    [MarshallStore.Comment].[ProductId],
    [MarshallStore.Comment].[Text]
FROM 
    [MarshallStore.Comment]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [MarshallStore.Comment].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [MarshallStore.Comment].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
WHERE
    1=1
    AND (@QueryString = '' 
        OR ([MarshallStore.Comment].[CommentId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Comment].[Active] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Comment].[DateTimeCreation] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Comment].[DateTimeLastModification] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Comment].[UserCreationId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Comment].[UserLastModificationId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Comment].[ProductId] LIKE  '%' + @QueryString + '%')
        OR ([MarshallStore.Comment].[Text] LIKE  '%' + @QueryString + '%')

    )
ORDER BY 
    CASE WHEN (@SorterColumn = 'CommentId' AND @SortToggler = 0) THEN [MarshallStore.Comment].[CommentId] END ASC,
    CASE WHEN (@SorterColumn = 'CommentId' AND @SortToggler = 1) THEN [MarshallStore.Comment].[CommentId] END DESC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 0) THEN [MarshallStore.Comment].[Active] END ASC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 1) THEN [MarshallStore.Comment].[Active] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 0) THEN [MarshallStore.Comment].[DateTimeCreation] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 1) THEN [MarshallStore.Comment].[DateTimeCreation] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 0) THEN [MarshallStore.Comment].[DateTimeLastModification] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 1) THEN [MarshallStore.Comment].[DateTimeLastModification] END DESC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 0) THEN [MarshallStore.Comment].[UserCreationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 1) THEN [MarshallStore.Comment].[UserCreationId] END DESC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 0) THEN [MarshallStore.Comment].[UserLastModificationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 1) THEN [MarshallStore.Comment].[UserLastModificationId] END DESC,
    CASE WHEN (@SorterColumn = 'ProductId' AND @SortToggler = 0) THEN [MarshallStore.Comment].[ProductId] END ASC,
    CASE WHEN (@SorterColumn = 'ProductId' AND @SortToggler = 1) THEN [MarshallStore.Comment].[ProductId] END DESC

OFFSET (@ActualPageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY
SELECT @TotalRows = COUNT(*) FROM [MarshallStore.Comment]