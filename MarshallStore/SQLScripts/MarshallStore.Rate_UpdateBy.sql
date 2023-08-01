CREATE PROCEDURE [dbo].[MarshallStore.Rate.UpdateByRateId]
(
    @RateId INT,
    @Active TINYINT,
    @DateTimeCreation DATETIME,
    @DateTimeLastModification DATETIME,
    @UserCreationId INT,
    @UserLastModificationId INT,
    @ProductId INT,
    @Score INT,

    @RowsAffected INT OUTPUT
)

AS

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2023
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

/*
 * Execute this stored procedure with the next script as example
 *
DECLARE	@RowsAffected int
EXEC [dbo].[MarshallStore.Rate.UpdateByRateId]
    @RateId = 1,
    @RowsAffected = @RowsAffected OUTPUT
SELECT @RowsAffected AS N'@RowsAffected'
 *
 */

--Last modification on: 31/07/2023 14:25:32

UPDATE [MarshallStore.Rate] SET
    [Active] = @Active,
    [DateTimeCreation] = @DateTimeCreation,
    [DateTimeLastModification] = @DateTimeLastModification,
    [UserCreationId] = @UserCreationId,
    [UserLastModificationId] = @UserLastModificationId,
    [ProductId] = @ProductId,
    [Score] = @Score
WHERE 
    1 = 1 
    AND [MarshallStore.Rate].[RateId] = @RateId 

SELECT @RowsAffected = @@ROWCOUNT