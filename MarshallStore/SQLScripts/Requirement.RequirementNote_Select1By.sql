CREATE PROCEDURE [dbo].[Requirement.RequirementNote.Select1ByRequirementNoteId]
(
    @RequirementNoteId INT
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

/*
 * Execute this stored procedure with the next script as example
 *
EXEC [dbo].[RequirementNote.Select1ByRequirementNoteId]
    @RequirementNoteId = 1
 *
 */

--Last modification on: 24/12/2022 6:47:58

SET DATEFORMAT DMY

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
    1 = 1
    AND [Requirement.RequirementNote].[RequirementNoteId] = @RequirementNoteId
ORDER BY 
    [Requirement.RequirementNote].[RequirementNoteId]