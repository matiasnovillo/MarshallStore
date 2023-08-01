CREATE PROCEDURE [dbo].[Requirement.RequirementPriority.SelectAllPaged]
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
EXEC [dbo].[Requirement.RequirementPriority.SelectAllPaged]
    
    @QueryString = N'',
    @ActualPageNumber = 1,
    @RowsPerPage = 10,
    @SorterColumn = N'RequirementPriorityId',
    @SortToggler = 0,
    @TotalRows = @TotalRows OUTPUT

SELECT @TotalRows AS N'@TotalRows'
*/

--Last modification on: 24/12/2022 6:47:07

SET DATEFORMAT DMY
SET NOCOUNT ON

SELECT
    [Requirement.RequirementPriority].[RequirementPriorityId],
    [Requirement.RequirementPriority].[Active],
    [Requirement.RequirementPriority].[DateTimeCreation],
    [Requirement.RequirementPriority].[DateTimeLastModification],
    [Requirement.RequirementPriority].[UserCreationId],
    [Requirement.RequirementPriority].[UserLastModificationId],
    [Requirement.RequirementPriority].[Name],
    [Requirement.RequirementPriority].[Description]
FROM 
    [Requirement.RequirementPriority]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [Requirement.RequirementPriority].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [Requirement.RequirementPriority].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
WHERE
    1=1
    AND (@QueryString = '' 
        OR ([Requirement.RequirementPriority].[RequirementPriorityId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementPriority].[Active] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementPriority].[DateTimeCreation] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementPriority].[DateTimeLastModification] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementPriority].[UserCreationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementPriority].[UserLastModificationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementPriority].[Name] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementPriority].[Description] LIKE  '%' + @QueryString + '%')

    )
ORDER BY 
    CASE WHEN (@SorterColumn = 'RequirementPriorityId' AND @SortToggler = 0) THEN [Requirement.RequirementPriority].[RequirementPriorityId] END ASC,
    CASE WHEN (@SorterColumn = 'RequirementPriorityId' AND @SortToggler = 1) THEN [Requirement.RequirementPriority].[RequirementPriorityId] END DESC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 0) THEN [Requirement.RequirementPriority].[Active] END ASC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 1) THEN [Requirement.RequirementPriority].[Active] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 0) THEN [Requirement.RequirementPriority].[DateTimeCreation] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 1) THEN [Requirement.RequirementPriority].[DateTimeCreation] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 0) THEN [Requirement.RequirementPriority].[DateTimeLastModification] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 1) THEN [Requirement.RequirementPriority].[DateTimeLastModification] END DESC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 0) THEN [Requirement.RequirementPriority].[UserCreationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 1) THEN [Requirement.RequirementPriority].[UserCreationId] END DESC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 0) THEN [Requirement.RequirementPriority].[UserLastModificationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 1) THEN [Requirement.RequirementPriority].[UserLastModificationId] END DESC,
    CASE WHEN (@SorterColumn = 'Name' AND @SortToggler = 0) THEN [Requirement.RequirementPriority].[Name] END ASC,
    CASE WHEN (@SorterColumn = 'Name' AND @SortToggler = 1) THEN [Requirement.RequirementPriority].[Name] END DESC,
    CASE WHEN (@SorterColumn = 'Description' AND @SortToggler = 0) THEN [Requirement.RequirementPriority].[Description] END ASC,
    CASE WHEN (@SorterColumn = 'Description' AND @SortToggler = 1) THEN [Requirement.RequirementPriority].[Description] END DESC

OFFSET (@ActualPageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY
SELECT @TotalRows = COUNT(*) FROM [Requirement.RequirementPriority]