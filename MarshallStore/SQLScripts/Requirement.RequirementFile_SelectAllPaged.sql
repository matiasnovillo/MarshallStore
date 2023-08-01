CREATE PROCEDURE [dbo].[Requirement.RequirementFile.SelectAllPaged]
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
EXEC [dbo].[Requirement.RequirementFile.SelectAllPaged]
    
    @QueryString = N'',
    @ActualPageNumber = 1,
    @RowsPerPage = 10,
    @SorterColumn = N'RequirementFileId',
    @SortToggler = 0,
    @TotalRows = @TotalRows OUTPUT

SELECT @TotalRows AS N'@TotalRows'
*/

--Last modification on: 29/12/2022 10:16:50

SET DATEFORMAT DMY
SET NOCOUNT ON

SELECT
    [Requirement.RequirementFile].[RequirementFileId],
    [Requirement.RequirementFile].[Active],
    [Requirement.RequirementFile].[DateTimeCreation],
    [Requirement.RequirementFile].[DateTimeLastModification],
    [Requirement.RequirementFile].[UserCreationId],
    [Requirement.RequirementFile].[UserLastModificationId],
    [Requirement.RequirementFile].[RequirementId],
    [Requirement.RequirementFile].[FilePath]
FROM 
    [Requirement.RequirementFile]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [Requirement.RequirementFile].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [Requirement.RequirementFile].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
WHERE
    1=1
    AND (@QueryString = '' 
        OR ([Requirement.RequirementFile].[RequirementFileId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementFile].[Active] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementFile].[DateTimeCreation] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementFile].[DateTimeLastModification] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementFile].[UserCreationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementFile].[UserLastModificationId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementFile].[RequirementId] LIKE  '%' + @QueryString + '%')
        OR ([Requirement.RequirementFile].[FilePath] LIKE  '%' + @QueryString + '%')

    )
ORDER BY 
    CASE WHEN (@SorterColumn = 'RequirementFileId' AND @SortToggler = 0) THEN [Requirement.RequirementFile].[RequirementFileId] END ASC,
    CASE WHEN (@SorterColumn = 'RequirementFileId' AND @SortToggler = 1) THEN [Requirement.RequirementFile].[RequirementFileId] END DESC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 0) THEN [Requirement.RequirementFile].[Active] END ASC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 1) THEN [Requirement.RequirementFile].[Active] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 0) THEN [Requirement.RequirementFile].[DateTimeCreation] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 1) THEN [Requirement.RequirementFile].[DateTimeCreation] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 0) THEN [Requirement.RequirementFile].[DateTimeLastModification] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 1) THEN [Requirement.RequirementFile].[DateTimeLastModification] END DESC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 0) THEN [Requirement.RequirementFile].[UserCreationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 1) THEN [Requirement.RequirementFile].[UserCreationId] END DESC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 0) THEN [Requirement.RequirementFile].[UserLastModificationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 1) THEN [Requirement.RequirementFile].[UserLastModificationId] END DESC,
    CASE WHEN (@SorterColumn = 'RequirementId' AND @SortToggler = 0) THEN [Requirement.RequirementFile].[RequirementId] END ASC,
    CASE WHEN (@SorterColumn = 'RequirementId' AND @SortToggler = 1) THEN [Requirement.RequirementFile].[RequirementId] END DESC,
    CASE WHEN (@SorterColumn = 'FilePath' AND @SortToggler = 0) THEN [Requirement.RequirementFile].[FilePath] END ASC,
    CASE WHEN (@SorterColumn = 'FilePath' AND @SortToggler = 1) THEN [Requirement.RequirementFile].[FilePath] END DESC

OFFSET (@ActualPageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY
SELECT @TotalRows = COUNT(*) FROM [Requirement.RequirementFile]terColumn = 'FilePath' AND @SortToggler = 0) THEN [Requirement.RequirementFile].[FilePath] END ASC,
    CASE WHEN (@SorterColumn = 'FilePath' AND @SortToggler = 1) THEN [Requirement.RequirementFile].[FilePath] END DESC

OFFSET (@ActualPageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY
SELECT @TotalRows = COUNT(*) FROM [Requirement.RequirementFile]