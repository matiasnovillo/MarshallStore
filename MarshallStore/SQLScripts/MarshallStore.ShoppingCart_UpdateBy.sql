CREATE PROCEDURE [dbo].[MarshallStore.ShoppingCart.UpdateByShoppingCartId]
(
    @ShoppingCartId INT,
    @Active TINYINT,
    @DateTimeCreation DATETIME,
    @DateTimeLastModification DATETIME,
    @UserCreationId INT,
    @UserLastModificationId INT,
    @ProductId INT,
    @Counter INT,

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
EXEC [dbo].[MarshallStore.ShoppingCart.UpdateByShoppingCartId]
    @ShoppingCartId = 1,
    @RowsAffected = @RowsAffected OUTPUT
SELECT @RowsAffected AS N'@RowsAffected'
 *
 */

--Last modification on: 31/07/2023 14:45:15

UPDATE [MarshallStore.ShoppingCart] SET
    [Active] = @Active,
    [DateTimeCreation] = @DateTimeCreation,
    [DateTimeLastModification] = @DateTimeLastModification,
    [UserCreationId] = @UserCreationId,
    [UserLastModificationId] = @UserLastModificationId,
    [ProductId] = @ProductId,
    [Counter] = @Counter
WHERE 
    1 = 1 
    AND [MarshallStore.ShoppingCart].[ShoppingCartId] = @ShoppingCartId 

SELECT @RowsAffected = @@ROWCOUNT