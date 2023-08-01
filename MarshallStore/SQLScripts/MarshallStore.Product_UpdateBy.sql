CREATE PROCEDURE [dbo].[MarshallStore.Product.UpdateByProductId]
(
    @ProductId INT,
    @Active TINYINT,
    @DateTimeCreation DATETIME,
    @DateTimeLastModification DATETIME,
    @UserCreationId INT,
    @UserLastModificationId INT,
    @ProductCategoryId INT,
    @Producer VARCHAR(300),
    @Model VARCHAR(300),
    @Price NUMERIC(24,6),
    @Description TEXT,
    @Image1 VARCHAR(8000),
    @Image2 VARCHAR(8000),
    @Image3 VARCHAR(8000),

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
EXEC [dbo].[MarshallStore.Product.UpdateByProductId]
    @ProductId = 1,
    @RowsAffected = @RowsAffected OUTPUT
SELECT @RowsAffected AS N'@RowsAffected'
 *
 */

--Last modification on: 31/07/2023 14:32:13

UPDATE [MarshallStore.Product] SET
    [Active] = @Active,
    [DateTimeCreation] = @DateTimeCreation,
    [DateTimeLastModification] = @DateTimeLastModification,
    [UserCreationId] = @UserCreationId,
    [UserLastModificationId] = @UserLastModificationId,
    [ProductCategoryId] = @ProductCategoryId,
    [Producer] = @Producer,
    [Model] = @Model,
    [Price] = @Price,
    [Description] = @Description,
    [Image1] = @Image1,
    [Image2] = @Image2,
    [Image3] = @Image3
WHERE 
    1 = 1 
    AND [MarshallStore.Product].[ProductId] = @ProductId 

SELECT @RowsAffected = @@ROWCOUNT