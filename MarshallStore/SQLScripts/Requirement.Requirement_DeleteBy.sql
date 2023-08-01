CREATE PROCEDURE [dbo].[Requirement.Requirement.DeleteByRequirementId]
(
    @RequirementId INT,
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
EXEC [dbo].[Requirement.Requirement.DeleteByRequirementId]
    @RequirementId = 1,
    @RowsAffected = @RowsAffected OUTPUT
SELECT @RowsAffected AS N'@RowsAffected'
 *
 */

--Last modification on: 27/12/2022 20:52:57

DELETE FROM 
    [Requirement.Requirement]
WHERE 
    1 = 1
    AND [Requirement.Requirement].[RequirementId] = @RequirementId

SELECT @RowsAffected = @@ROWCOUNT