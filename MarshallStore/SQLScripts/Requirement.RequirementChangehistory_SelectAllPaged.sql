CREATE PROCEDURE [dbo].[Requirement.RequirementChangehistory.SelectAllPaged]
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
EXEC [dbo].[Requirement.RequirementChangehistory.SelectAllPaged]
    
    @QueryString = N'',
    @ActualPageNumber = 1,
    @RowsPerPage = 10,
    @SorterColumn = N'RequirementChangehistoryId',
    @SortToggler = 0,
    @TotalRows = @TotalRows OUTPUT

SELECT @TotalRows AS N'@TotalRows'
*/

--Last modification on: 24/12/2022 6:48:12

SET DATEFORMAT DMY
SET NOCOUNT ON

SELECT
    [Requirement.RequirementChangehistory].[RequirementChangehistoryId],
    [Requirement.RequirementChangehistory].[Active],
    [Requirement.RequirementChangehistory].[DateTimeCreation],
    [Requirement.RequirementChangehistory].[DateTimeLastModification],
    [Requirement.RequirementChangehistory].[UserCreationId],
    [Requirement.RequirementChangehistory].[UserLastModificationId],
    [Requirement.RequirementChangehistory].[RequirementId],
    [Requirement.RequirementChangehistory].[RequirementStateId],
    [Requirement.RequirementChangehistory].[RequirementPriorityId]
FROM 
    [Requirement.RequirementChangehistory]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [Requirement.RequirementChangehistory].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [Requirement.RequirementChangehistory].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
WHERE
    1=1
    AND (@QueryString = '' 
        OR ([Requirement.RequirementChangehistory].[RequirementChangehistoryId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementChangehistory].[Active] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementChangehistory].[DateTimeCreation] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementChangehistory].[DateTimeLastModification] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementChangehistory].[UserCreationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementChangehistory].[UserLastModificationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementChangehistory].[RequirementId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementChangehistory].[RequirementStateId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementChangehistory].[RequirementPriorityId] LIKE  '%' + @QueryString + '%')

    )
ORDER BY 
    CASE WHEN (@SorterColumn = 'RequirementChangehistoryId' AND @SortToggler = 0) THEN [Requirement.RequirementChangehistory].[RequirementChangehistoryId] END ASC,
    CASE WHEN (@SorterColumn = 'RequirementChangehistoryId' AND @SortToggler = 1) THEN [Requirement.RequirementChangehistory].[RequirementChangehistoryId] END DESC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 0) THEN [Requirement.RequirementChangehistory].[Active] END ASC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 1) THEN [Requirement.RequirementChangehistory].[Active] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 0) THEN [Requirement.RequirementChangehistory].[DateTimeCreation] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 1) THEN [Requirement.RequirementChangehistory].[DateTimeCreation] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 0) THEN [Requirement.RequirementChangehistory].[DateTimeLastModification] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 1) THEN [Requirement.RequirementChangehistory].[DateTimeLastModification] END DESC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 0) THEN [Requirement.RequirementChangehistory].[UserCreationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 1) THEN [Requirement.RequirementChangehistory].[UserCreationId] END DESC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 0) THEN [Requirement.RequirementChangehistory].[UserLastModificationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 1) THEN [Requirement.RequirementChangehistory].[UserLastModificationId] END DESC,
    CASE WHEN (@SorterColumn = 'RequirementId' AND @SortToggler = 0) THEN [Requirement.RequirementChangehistory].[RequirementId] END ASC,
    CASE WHEN (@SorterColumn = 'RequirementId' AND @SortToggler = 1) THEN [Requirement.RequirementChangehistory].[RequirementId] END DESC,
    CASE WHEN (@SorterColumn = 'RequirementStateId' AND @SortToggler = 0) THEN [Requirement.RequirementChangehistory].[RequirementStateId] END ASC,
    CASE WHEN (@SorterColumn = 'RequirementStateId' AND @SortToggler = 1) THEN [Requirement.RequirementChangehistory].[RequirementStateId] END DESC,
    CASE WHEN (@SorterColumn = 'RequirementPriorityId' AND @SortToggler = 0) THEN [Requirement.RequirementChangehistory].[RequirementPriorityId] END ASC,
    CASE WHEN (@SorterColumn = 'RequirementPriorityId' AND @SortToggler = 1) THEN [Requirement.RequirementChangehistory].[RequirementPriorityId] END DESC

OFFSET (@ActualPageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY
SELECT @TotalRows = COUNT(*) FROM [Requirement.RequirementChangehistory]