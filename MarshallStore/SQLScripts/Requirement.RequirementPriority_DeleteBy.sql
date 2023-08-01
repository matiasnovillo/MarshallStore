CREATE PROCEDURE [dbo].[Requirement.RequirementPriority.DeleteByRequirementPriorityId]
(
    @RequirementPriorityId INT,
    @RowsAffected INT OUTPUT
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
DECLARE	@RowsAffected INT
EXEC [dbo].[Requirement.RequirementPriority.DeleteByRequirementPriorityId]
    @RequirementPriorityId = 1,
    @RowsAffected = @RowsAffected OUTPUT
SELECT @RowsAffected AS N'@RowsAffected'
 *
 */

--Last modification on: 24/12/2022 6:47:07

DELETE FROM 
    [Requirement.RequirementPriority]
WHERE 
    1 = 1
    AND [Requirement.RequirementPriority].[RequirementPriorityId] = @RequirementPriorityId

SELECT @RowsAffected = @@ROWCOUNT