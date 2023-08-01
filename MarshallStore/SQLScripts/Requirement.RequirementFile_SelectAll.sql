CREATE PROCEDURE [dbo].[Requirement.RequirementFile.SelectAll]

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

/*
 * Execute this stored procedure with the next script as example
 *
EXEC [dbo].[Requirement.RequirementFile.SelectAll]
 *
 */

--Last modification on: 29/12/2022 10:16:50

SET DATEFORMAT DMY

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
ORDER BY 
    [Requirement.RequirementFile].[RequirementFileId]quirement.RequirementFile].[RequirementFileId]