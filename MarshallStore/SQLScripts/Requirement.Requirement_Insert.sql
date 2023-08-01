CREATE PROCEDURE [dbo].[Requirement.Requirement.Insert] 
(
    @Active TINYINT,
    @DateTimeCreation DATETIME,
    @DateTimeLastModification DATETIME,
    @UserCreationId INT,
    @UserLastModificationId INT,
    @Title VARCHAR(100),
    @Body VARCHAR(8000),
    @RequirementStateId INT,
    @RequirementPriorityId INT,
    @UserEmployeeId INT,

    @NewEnteredId INT OUTPUT
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
DECLARE	@NewEnteredId INT
EXEC [dbo].[Requirement.Requirement.Insert]

    @Active = 1,
    @DateTimeCreation = N'01/01/1753 0:00:00.001',
    @DateTimeLastModification = N'01/01/1753 0:00:00.001',
    @UserCreationId = 1,
    @UserLastModificationId = 1,
    @Title = N'PutTitle',
    @Body = N'PutBody',
     @RequirementStateId = 1,
     @RequirementPriorityId = 1,
     @UserEmployeeId = 1,

@NewEnteredId = @NewEnteredId OUTPUT

SELECT @NewEnteredId AS N'@NewEnteredId'
 *
 */

--Last modification on: 27/12/2022 20:52:57

INSERT INTO [Requirement.Requirement]
(
    [Active],
    [DateTimeCreation],
    [DateTimeLastModification],
    [UserCreationId],
    [UserLastModificationId],
    [Title],
    [Body],
    [RequirementStateId],
    [RequirementPriorityId],
    [UserEmployeeId]
)
VALUES
(
    @Active,
    @DateTimeCreation,
    @DateTimeLastModification,
    @UserCreationId,
    @UserLastModificationId,
    @Title,
    @Body,
    @RequirementStateId,
    @RequirementPriorityId,
    @UserEmployeeId
)

SELECT @NewEnteredId = @@IDENTITYtionId,
    @ClientId,
    @Title,
    @Body,
    @RequirementStateId,
    @RequirementTypeId,
    @RequirementPriorityId,
    @UserProgrammerId
)

SELECT @NewEnteredId = @@IDENTITY