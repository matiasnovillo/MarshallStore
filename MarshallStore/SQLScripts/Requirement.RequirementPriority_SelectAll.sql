CREATE PROCEDURE [dbo].[Requirement.RequirementPriority.SelectAll]

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
EXEC [dbo].[Requirement.RequirementPriority.SelectAll]
 *
 */

--Last modification on: 24/12/2022 6:47:07

SET DATEFORMAT DMY

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
ORDER BY 
    [Requirement.RequirementPriority].[RequirementPriorityId]