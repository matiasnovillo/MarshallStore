CREATE PROCEDURE [dbo].[Requirement.RequirementState.UpdateByRequirementStateId]
(
    @RequirementStateId INT,
    @Active TINYINT,
    @DateTimeCreation DATETIME,
    @DateTimeLastModification DATETIME,
    @UserCreationId INT,
    @UserLastModificationId INT,
    @Name VARCHAR(100),

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
DECLARE	@RowsAffected int
EXEC [dbo].[Requirement.RequirementState.UpdateByRequirementStateId]
    @RequirementStateId = 1,
    @RowsAffected = @RowsAffected OUTPUT
SELECT @RowsAffected AS N'@RowsAffected'
 *
 */

--Last modification on: 24/12/2022 6:47:04

UPDATE [Requirement.RequirementState] SET
    [Active] = @Active,
    [DateTimeCreation] = @DateTimeCreation,
    [DateTimeLastModification] = @DateTimeLastModification,
    [UserCreationId] = @UserCreationId,
    [UserLastModificationId] = @UserLastModificationId,
    [Name] = @Name
WHERE 
    1 = 1 
    AND [Requirement.RequirementState].[RequirementStateId] = @RequirementStateId 

SELECT @RowsAffected = @@ROWCOUNT