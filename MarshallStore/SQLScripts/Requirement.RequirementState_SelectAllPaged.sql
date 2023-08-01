CREATE PROCEDURE [dbo].[Requirement.RequirementState.SelectAllPaged]
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
EXEC [dbo].[Requirement.RequirementState.SelectAllPaged]
    
    @QueryString = N'',
    @ActualPageNumber = 1,
    @RowsPerPage = 10,
    @SorterColumn = N'RequirementStateId',
    @SortToggler = 0,
    @TotalRows = @TotalRows OUTPUT

SELECT @TotalRows AS N'@TotalRows'
*/

--Last modification on: 24/12/2022 6:47:04

SET DATEFORMAT DMY
SET NOCOUNT ON

SELECT
    [Requirement.RequirementState].[RequirementStateId],
    [Requirement.RequirementState].[Active],
    [Requirement.RequirementState].[DateTimeCreation],
    [Requirement.RequirementState].[DateTimeLastModification],
    [Requirement.RequirementState].[UserCreationId],
    [Requirement.RequirementState].[UserLastModificationId],
    [Requirement.RequirementState].[Name]
FROM 
    [Requirement.RequirementState]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [Requirement.RequirementState].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [Requirement.RequirementState].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
WHERE
    1=1
    AND (@QueryString = '' 
        OR ([Requirement.RequirementState].[RequirementStateId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementState].[Active] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementState].[DateTimeCreation] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementState].[DateTimeLastModification] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementState].[UserCreationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementState].[UserLastModificationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementState].[Name] LIKE  '%' + @QueryString + '%')

    )
ORDER BY 
    CASE WHEN (@SorterColumn = 'RequirementStateId' AND @SortToggler = 0) THEN [Requirement.RequirementState].[RequirementStateId] END ASC,
    CASE WHEN (@SorterColumn = 'RequirementStateId' AND @SortToggler = 1) THEN [Requirement.RequirementState].[RequirementStateId] END DESC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 0) THEN [Requirement.RequirementState].[Active] END ASC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 1) THEN [Requirement.RequirementState].[Active] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 0) THEN [Requirement.RequirementState].[DateTimeCreation] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 1) THEN [Requirement.RequirementState].[DateTimeCreation] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 0) THEN [Requirement.RequirementState].[DateTimeLastModification] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 1) THEN [Requirement.RequirementState].[DateTimeLastModification] END DESC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 0) THEN [Requirement.RequirementState].[UserCreationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 1) THEN [Requirement.RequirementState].[UserCreationId] END DESC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 0) THEN [Requirement.RequirementState].[UserLastModificationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 1) THEN [Requirement.RequirementState].[UserLastModificationId] END DESC,
    CASE WHEN (@SorterColumn = 'Name' AND @SortToggler = 0) THEN [Requirement.RequirementState].[Name] END ASC,
    CASE WHEN (@SorterColumn = 'Name' AND @SortToggler = 1) THEN [Requirement.RequirementState].[Name] END DESC

OFFSET (@ActualPageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY
SELECT @TotalRows = COUNT(*) FROM [Requirement.RequirementState]