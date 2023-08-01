CREATE PROCEDURE [dbo].[Requirement.Requirement.SelectAllPaged]
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
EXEC [dbo].[Requirement.Requirement.SelectAllPaged]
    
    @QueryString = N'',
    @ActualPageNumber = 1,
    @RowsPerPage = 10,
    @SorterColumn = N'RequirementId',
    @SortToggler = 0,
    @TotalRows = @TotalRows OUTPUT

SELECT @TotalRows AS N'@TotalRows'
*/

--Last modification on: 27/12/2022 20:52:58

SET DATEFORMAT DMY
SET NOCOUNT ON

SELECT
    [Requirement.Requirement].[RequirementId],
    [Requirement.Requirement].[Active],
    [Requirement.Requirement].[DateTimeCreation],
    [Requirement.Requirement].[DateTimeLastModification],
    [Requirement.Requirement].[UserCreationId],
    [Requirement.Requirement].[UserLastModificationId],
    [Requirement.Requirement].[Title],
    [Requirement.Requirement].[Body],
    [Requirement.Requirement].[RequirementStateId],
    [Requirement.Requirement].[RequirementPriorityId],
    [Requirement.Requirement].[UserEmployeeId]
FROM 
    [Requirement.Requirement]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [Requirement.Requirement].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [Requirement.Requirement].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
WHERE
    1=1
    AND (@QueryString = '' 
        OR ([Requirement.Requirement].[RequirementId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Requirement].[Active] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Requirement].[DateTimeCreation] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Requirement].[DateTimeLastModification] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Requirement].[UserCreationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Requirement].[UserLastModificationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Requirement].[Title] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Requirement].[Body] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Requirement].[RequirementStateId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Requirement].[RequirementPriorityId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.Requirement].[UserEmployeeId] LIKE  '%' + @QueryString + '%')

    )
ORDER BY 
    CASE WHEN (@SorterColumn = 'RequirementId' AND @SortToggler = 0) THEN [Requirement.Requirement].[RequirementId] END ASC,
    CASE WHEN (@SorterColumn = 'RequirementId' AND @SortToggler = 1) THEN [Requirement.Requirement].[RequirementId] END DESC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 0) THEN [Requirement.Requirement].[Active] END ASC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 1) THEN [Requirement.Requirement].[Active] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 0) THEN [Requirement.Requirement].[DateTimeCreation] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 1) THEN [Requirement.Requirement].[DateTimeCreation] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 0) THEN [Requirement.Requirement].[DateTimeLastModification] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 1) THEN [Requirement.Requirement].[DateTimeLastModification] END DESC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 0) THEN [Requirement.Requirement].[UserCreationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 1) THEN [Requirement.Requirement].[UserCreationId] END DESC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 0) THEN [Requirement.Requirement].[UserLastModificationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 1) THEN [Requirement.Requirement].[UserLastModificationId] END DESC,
    CASE WHEN (@SorterColumn = 'Title' AND @SortToggler = 0) THEN [Requirement.Requirement].[Title] END ASC,
    CASE WHEN (@SorterColumn = 'Title' AND @SortToggler = 1) THEN [Requirement.Requirement].[Title] END DESC,
    CASE WHEN (@SorterColumn = 'Body' AND @SortToggler = 0) THEN [Requirement.Requirement].[Body] END ASC,
    CASE WHEN (@SorterColumn = 'Body' AND @SortToggler = 1) THEN [Requirement.Requirement].[Body] END DESC,
    CASE WHEN (@SorterColumn = 'RequirementStateId' AND @SortToggler = 0) THEN [Requirement.Requirement].[RequirementStateId] END ASC,
    CASE WHEN (@SorterColumn = 'RequirementStateId' AND @SortToggler = 1) THEN [Requirement.Requirement].[RequirementStateId] END DESC,
    CASE WHEN (@SorterColumn = 'RequirementPriorityId' AND @SortToggler = 0) THEN [Requirement.Requirement].[RequirementPriorityId] END ASC,
    CASE WHEN (@SorterColumn = 'RequirementPriorityId' AND @SortToggler = 1) THEN [Requirement.Requirement].[RequirementPriorityId] END DESC,
    CASE WHEN (@SorterColumn = 'UserEmployeeId' AND @SortToggler = 0) THEN [Requirement.Requirement].[UserEmployeeId] END ASC,
    CASE WHEN (@SorterColumn = 'UserEmployeeId' AND @SortToggler = 1) THEN [Requirement.Requirement].[UserEmployeeId] END DESC

OFFSET (@ActualPageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY
SELECT @TotalRows = COUNT(*) FROM [Requirement.Requirement]AND @SortToggler = 1) THEN [Requirement.Requirement].[RequirementTypeId] END DESC,
    CASE WHEN (@SorterColumn = 'RequirementPriorityId' AND @SortToggler = 0) THEN [Requirement.Requirement].[RequirementPriorityId] END ASC,
    CASE WHEN (@SorterColumn = 'RequirementPriorityId' AND @SortToggler = 1) THEN [Requirement.Requirement].[RequirementPriorityId] END DESC,
    CASE WHEN (@SorterColumn = 'UserProgrammerId' AND @SortToggler = 0) THEN [Requirement.Requirement].[UserProgrammerId] END ASC,
    CASE WHEN (@SorterColumn = 'UserProgrammerId' AND @SortToggler = 1) THEN [Requirement.Requirement].[UserProgrammerId] END DESC

OFFSET (@ActualPageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY
SELECT @TotalRows = COUNT(*) FROM [Requirement.Requirement]