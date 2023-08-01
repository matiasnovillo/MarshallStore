CREATE PROCEDURE [dbo].[Examples.Example.SelectAll]

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

/*
 * Execute this stored procedure with the next script as example
 *
EXEC [dbo].[Examples.Example.SelectAll]
 *
 */

--Last modification on: 15/02/2023 16:56:40

SET DATEFORMAT DMY

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
ORDER BY 
    [Examples.Example].[ExampleId]