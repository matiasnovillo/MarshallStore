CREATE PROCEDURE [dbo].[Examples.Example.SelectAllPaged]
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
EXEC [dbo].[Examples.Example.SelectAllPaged]
    
    @QueryString = N'',
    @ActualPageNumber = 1,
    @RowsPerPage = 10,
    @SorterColumn = N'ExampleId',
    @SortToggler = 0,
    @TotalRows = @TotalRows OUTPUT

SELECT @TotalRows AS N'@TotalRows'
*/

--Last modification on: 15/02/2023 16:56:40

SET DATEFORMAT DMY
SET NOCOUNT ON

SELECT
    [Examples.Example].[ExampleId],
    [Examples.Example].[Active],
    [Examples.Example].[DateTimeCreation],
    [Examples.Example].[DateTimeLastModification],
    [Examples.Example].[UserCreationId],
    [Examples.Example].[UserLastModificationId],
    [Examples.Example].[Boolean],
    [Examples.Example].[DateTime],
    [Examples.Example].[Decimal],
    [Examples.Example].[Integer],
    [Examples.Example].[TextBasic],
    [Examples.Example].[TextEmail],
    [Examples.Example].[TextFile],
    [Examples.Example].[TextPassword],
    [Examples.Example].[TextPhoneNumber],
    [Examples.Example].[TextTag],
    [Examples.Example].[TextTextArea],
    [Examples.Example].[TextTextEditor],
    [Examples.Example].[TextURL],
    [Examples.Example].[ForeignKeyDropDown],
    [Examples.Example].[ForeignKeyOption],
    [Examples.Example].[TextHexColour],
    [Examples.Example].[Time]
FROM 
    [Examples.Example]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [Examples.Example].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [Examples.Example].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
WHERE
    1=1
    AND (@QueryString = '' 
        OR ([Examples.Example].[ExampleId] LIKE  '%' + @QueryString + '%')
        OR ([Examples.Example].[Active] LIKE  '%' + @QueryString + '%')
        OR ([Examples.Example].[DateTimeCreation] LIKE  '%' + @QueryString + '%')
        OR ([Examples.Example].[DateTimeLastModification] LIKE  '%' + @QueryString + '%')
        OR ([Examples.Example].[UserCreationId] LIKE  '%' + @QueryString + '%')
        OR ([Examples.Example].[UserLastModificationId] LIKE  '%' + @QueryString + '%')
        OR ([Examples.Example].[Boolean] LIKE  '%' + @QueryString + '%')
        OR ([Examples.Example].[DateTime] LIKE  '%' + @QueryString + '%')
        OR ([Examples.Example].[Decimal] LIKE  '%' + @QueryString + '%')
        OR ([Examples.Example].[Integer] LIKE  '%' + @QueryString + '%')
        OR ([Examples.Example].[TextBasic] LIKE  '%' + @QueryString + '%')
        OR ([Examples.Example].[TextEmail] LIKE  '%' + @QueryString + '%')
        OR ([Examples.Example].[TextFile] LIKE  '%' + @QueryString + '%')
        OR ([Examples.Example].[TextPassword] LIKE  '%' + @QueryString + '%')
        OR ([Examples.Example].[TextPhoneNumber] LIKE  '%' + @QueryString + '%')
        OR ([Examples.Example].[TextTag] LIKE  '%' + @QueryString + '%')
        OR ([Examples.Example].[TextTextArea] LIKE  '%' + @QueryString + '%')
        OR ([Examples.Example].[TextTextEditor] LIKE  '%' + @QueryString + '%')
        OR ([Examples.Example].[TextURL] LIKE  '%' + @QueryString + '%')
        OR ([Examples.Example].[ForeignKeyDropDown] LIKE  '%' + @QueryString + '%')
        OR ([Examples.Example].[ForeignKeyOption] LIKE  '%' + @QueryString + '%')
        OR ([Examples.Example].[TextHexColour] LIKE  '%' + @QueryString + '%')
        OR ([Examples.Example].[Time] LIKE  '%' + @QueryString + '%')

    )
ORDER BY 
    CASE WHEN (@SorterColumn = 'ExampleId' AND @SortToggler = 0) THEN [Examples.Example].[ExampleId] END ASC,
    CASE WHEN (@SorterColumn = 'ExampleId' AND @SortToggler = 1) THEN [Examples.Example].[ExampleId] END DESC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 0) THEN [Examples.Example].[Active] END ASC,
    CASE WHEN (@SorterColumn = 'Active' AND @SortToggler = 1) THEN [Examples.Example].[Active] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 0) THEN [Examples.Example].[DateTimeCreation] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeCreation' AND @SortToggler = 1) THEN [Examples.Example].[DateTimeCreation] END DESC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 0) THEN [Examples.Example].[DateTimeLastModification] END ASC,
    CASE WHEN (@SorterColumn = 'DateTimeLastModification' AND @SortToggler = 1) THEN [Examples.Example].[DateTimeLastModification] END DESC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 0) THEN [Examples.Example].[UserCreationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserCreationId' AND @SortToggler = 1) THEN [Examples.Example].[UserCreationId] END DESC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 0) THEN [Examples.Example].[UserLastModificationId] END ASC,
    CASE WHEN (@SorterColumn = 'UserLastModificationId' AND @SortToggler = 1) THEN [Examples.Example].[UserLastModificationId] END DESC,
    CASE WHEN (@SorterColumn = 'Boolean' AND @SortToggler = 0) THEN [Examples.Example].[Boolean] END ASC,
    CASE WHEN (@SorterColumn = 'Boolean' AND @SortToggler = 1) THEN [Examples.Example].[Boolean] END DESC,
    CASE WHEN (@SorterColumn = 'DateTime' AND @SortToggler = 0) THEN [Examples.Example].[DateTime] END ASC,
    CASE WHEN (@SorterColumn = 'DateTime' AND @SortToggler = 1) THEN [Examples.Example].[DateTime] END DESC,
    CASE WHEN (@SorterColumn = 'Decimal' AND @SortToggler = 0) THEN [Examples.Example].[Decimal] END ASC,
    CASE WHEN (@SorterColumn = 'Decimal' AND @SortToggler = 1) THEN [Examples.Example].[Decimal] END DESC,
    CASE WHEN (@SorterColumn = 'Integer' AND @SortToggler = 0) THEN [Examples.Example].[Integer] END ASC,
    CASE WHEN (@SorterColumn = 'Integer' AND @SortToggler = 1) THEN [Examples.Example].[Integer] END DESC,
    CASE WHEN (@SorterColumn = 'TextBasic' AND @SortToggler = 0) THEN [Examples.Example].[TextBasic] END ASC,
    CASE WHEN (@SorterColumn = 'TextBasic' AND @SortToggler = 1) THEN [Examples.Example].[TextBasic] END DESC,
    CASE WHEN (@SorterColumn = 'TextEmail' AND @SortToggler = 0) THEN [Examples.Example].[TextEmail] END ASC,
    CASE WHEN (@SorterColumn = 'TextEmail' AND @SortToggler = 1) THEN [Examples.Example].[TextEmail] END DESC,
    CASE WHEN (@SorterColumn = 'TextFile' AND @SortToggler = 0) THEN [Examples.Example].[TextFile] END ASC,
    CASE WHEN (@SorterColumn = 'TextFile' AND @SortToggler = 1) THEN [Examples.Example].[TextFile] END DESC,
    CASE WHEN (@SorterColumn = 'TextPassword' AND @SortToggler = 0) THEN [Examples.Example].[TextPassword] END ASC,
    CASE WHEN (@SorterColumn = 'TextPassword' AND @SortToggler = 1) THEN [Examples.Example].[TextPassword] END DESC,
    CASE WHEN (@SorterColumn = 'TextPhoneNumber' AND @SortToggler = 0) THEN [Examples.Example].[TextPhoneNumber] END ASC,
    CASE WHEN (@SorterColumn = 'TextPhoneNumber' AND @SortToggler = 1) THEN [Examples.Example].[TextPhoneNumber] END DESC,
    CASE WHEN (@SorterColumn = 'TextTag' AND @SortToggler = 0) THEN [Examples.Example].[TextTag] END ASC,
    CASE WHEN (@SorterColumn = 'TextTag' AND @SortToggler = 1) THEN [Examples.Example].[TextTag] END DESC,
    CASE WHEN (@SorterColumn = 'TextTextArea' AND @SortToggler = 0) THEN [Examples.Example].[TextTextArea] END ASC,
    CASE WHEN (@SorterColumn = 'TextTextArea' AND @SortToggler = 1) THEN [Examples.Example].[TextTextArea] END DESC,
    CASE WHEN (@SorterColumn = 'TextTextEditor' AND @SortToggler = 0) THEN [Examples.Example].[TextTextEditor] END ASC,
    CASE WHEN (@SorterColumn = 'TextTextEditor' AND @SortToggler = 1) THEN [Examples.Example].[TextTextEditor] END DESC,
    CASE WHEN (@SorterColumn = 'TextURL' AND @SortToggler = 0) THEN [Examples.Example].[TextURL] END ASC,
    CASE WHEN (@SorterColumn = 'TextURL' AND @SortToggler = 1) THEN [Examples.Example].[TextURL] END DESC,
    CASE WHEN (@SorterColumn = 'ForeignKeyDropDown' AND @SortToggler = 0) THEN [Examples.Example].[ForeignKeyDropDown] END ASC,
    CASE WHEN (@SorterColumn = 'ForeignKeyDropDown' AND @SortToggler = 1) THEN [Examples.Example].[ForeignKeyDropDown] END DESC,
    CASE WHEN (@SorterColumn = 'ForeignKeyOption' AND @SortToggler = 0) THEN [Examples.Example].[ForeignKeyOption] END ASC,
    CASE WHEN (@SorterColumn = 'ForeignKeyOption' AND @SortToggler = 1) THEN [Examples.Example].[ForeignKeyOption] END DESC,
    CASE WHEN (@SorterColumn = 'TextHexColour' AND @SortToggler = 0) THEN [Examples.Example].[TextHexColour] END ASC,
    CASE WHEN (@SorterColumn = 'TextHexColour' AND @SortToggler = 1) THEN [Examples.Example].[TextHexColour] END DESC,
    CASE WHEN (@SorterColumn = 'Time' AND @SortToggler = 0) THEN [Examples.Example].[Time] END ASC,
    CASE WHEN (@SorterColumn = 'Time' AND @SortToggler = 1) THEN [Examples.Example].[Time] END DESC

OFFSET (@ActualPageNumber - 1) * @RowsPerPage ROWS
FETCH NEXT @RowsPerPage ROWS ONLY
SELECT @TotalRows = COUNT(*) FROM [Examples.Example]mple]