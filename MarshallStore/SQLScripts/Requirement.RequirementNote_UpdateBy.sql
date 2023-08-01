CREATE PROCEDURE [dbo].[Requirement.RequirementNote.UpdateByRequirementNoteId]
(
    @RequirementNoteId INT,
    @Active TINYINT,
    @DateTimeCreation DATETIME,
    @DateTimeLastModification DATETIME,
    @UserCreationId INT,
    @UserLastModificationId INT,
    @Title VARCHAR(100),
    @Body VARCHAR(8000),

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
EXEC [dbo].[Requirement.RequirementNote.UpdateByRequirementNoteId]
    @RequirementNoteId = 1,
    @RowsAffected = @RowsAffected OUTPUT
SELECT @RowsAffected AS N'@RowsAffected'
 *
 */

--Last modification on: 24/12/2022 6:47:58

UPDATE [Requirement.RequirementNote] SET
    [Active] = @Active,
    [DateTimeCreation] = @DateTimeCreation,
    [DateTimeLastModification] = @DateTimeLastModification,
    [UserCreationId] = @UserCreationId,
    [UserLastModificationId] = @UserLastModificationId,
    [Title] = @Title,
    [Body] = @Body
WHERE 
    1 = 1 
    AND [Requirement.RequirementNote].[RequirementNoteId] = @RequirementNoteId 

SELECT @RowsAffected = @@ROWCOUNT