CREATE PROCEDURE [dbo].[MarshallStore.PurchaseProduct.DeleteByPurchaseProductId]
(
    @PurchaseProductId INT,
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
DECLARE	@RowsAffected INT
EXEC [dbo].[MarshallStore.PurchaseProduct.DeleteByPurchaseProductId]
    @PurchaseProductId = 1,
    @RowsAffected = @RowsAffected OUTPUT
SELECT @RowsAffected AS N'@RowsAffected'
 *
 */

--Last modification on: 31/07/2023 14:25:24

DELETE FROM 
    [MarshallStore.PurchaseProduct]
WHERE 
    1 = 1
    AND [MarshallStore.PurchaseProduct].[PurchaseProductId] = @PurchaseProductId

SELECT @RowsAffected = @@ROWCOUNT