CREATE PROCEDURE [dbo].[Requirement.RequirementNote.SelectAllPaged]
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
 * Copyright © 2022
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

/*Execute this stored procedure with the next script as example

DECLARE	@TotalRows int
EXEC [dbo].[Requirement.RequirementNote.SelectAllPaged]
    
    @QueryString = N'',
    @ActualPageNumber = 1,
    @RowsPerPage = 10,
    @SorterColumn = N'RequirementNoteId',
    @SortToggler = 0,
    @TotalRows = @TotalRows OUTPUT

SELECT @TotalRows AS N'@TotalRows'
*/

--Last modification on: 24/12/2022 6:47:58

SET DATEFORMAT DMY
SET NOCOUNT ON

SELECT
    [Requirement.RequirementNote].[RequirementNoteId],
    [Requirement.RequirementNote].[Active],
    [Requirement.RequirementNote].[DateTimeCreation],
    [Requirement.RequirementNote].[DateTimeLastModification],
    [Requirement.RequirementNote].[UserCreationId],
    [Requirement.RequirementNote].[UserLastModificationId],
    [Requirement.RequirementNote].[Title],
    [Requirement.RequirementNote].[Body]
FROM 
    [Requirement.RequirementNote]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [Requirement.RequirementNote].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [Requirement.RequirementNote].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
WHERE
    1=1
    AND (@QueryString = '' 
        OR ([Requirement.RequirementNote].[RequirementNoteId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementNote].[Active] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementNote].[DateTimeCreation] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementNote].[DateTimeLastModification] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementNote].[UserCreationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementNote].[UserLastModificationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementNote].[Title] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementNote].[Body] LIKE  '%' + @QueryString + '%')

    )
ORDER BY 
    CASE WHEN (@SorterColumn = 'RequirementNoteId' AND @SortToggler = 0) THEN [Requirement.RequirementNote].[RequirementNoteId] END ASC,
    CASE WHEN (@SorterColumn = 'RequirementNoteId' AND @SortToggler = 1) THEN [Requirement.RequirementNote].[RequirementNoteId] END DESC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 0) THEN [Requirement.RequirementNote].[Active] END ASC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 1) THEN [Requirement.RequirementNote].[Active] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 0) THEN [Requirement.RequirementNote].[DateTimeCreation] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 1) THEN [Requirement.RequirementNote].[DateTimeCreation] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 0) THEN [Requirement.RequirementNote].[DateTimeLastModification] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 1) THEN [Requirement.RequirementNote].[DateTimeLastModification] END DESC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 0) THEN [Requirement.RequirementNote].[UserCreationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 1) THEN [Requirement.RequirementNote].[UserCreationId] END DESC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 0) THEN [Requirement.RequirementNote].[UserLastModificationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 1) THEN [Requirement.RequirementNote].[UserLastModificationId] END DESC,
    CASE WHEN (@SorterColumn = 'Title' AND @SortToggler = 0) THEN [Requirement.RequirementNote].[Title] END ASC,
    CASE WHEN (@SorterColumn = 'Title' AND @SortToggler = 1) THEN [Requirement.RequirementNote].[Title] END DESC,
    CASE WHEN (@SorterColumn = 'Body' AND @SortToggler = 0) THEN [Requirement.RequirementNote].[Body] END ASC,
    CASE WHEN (@SorterColumn = 'Body' AND @SortToggler = 1) THEN [Requirement.RequirementNote].[Body] END DESC

OFFSET (@ActualPageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY
SELECT @TotalRows = COUNT(*) FROM [Requirement.RequirementNote]