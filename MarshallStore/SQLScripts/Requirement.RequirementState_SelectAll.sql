CREATE PROCEDURE [dbo].[Requirement.RequirementState.SelectAll]

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
EXEC [dbo].[Requirement.RequirementState.SelectAll]
 *
 */

--Last modification on: 24/12/2022 6:47:04

SET DATEFORMAT DMY

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
ORDER BY 
    [Requirement.RequirementState].[RequirementStateId]