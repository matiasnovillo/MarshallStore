CREATE PROCEDURE [dbo].[Requirement.RequirementChangehistory.Select1ByRequirementChangehistoryId]
(
    @RequirementChangehistoryId INT
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
EXEC [dbo].[RequirementChangehistory.Select1ByRequirementChangehistoryId]
    @RequirementChangehistoryId = 1
 *
 */

--Last modification on: 24/12/2022 6:48:12

SET DATEFORMAT DMY

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
    1 = 1
    AND [Requirement.RequirementChangehistory].[RequirementChangehistoryId] = @RequirementChangehistoryId
ORDER BY 
    [Requirement.RequirementChangehistory].[RequirementChangehistoryId]