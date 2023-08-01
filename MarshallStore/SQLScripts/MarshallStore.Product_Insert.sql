CREATE PROCEDURE [dbo].[MarshallStore.Product.Insert] 
(
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

    @NewEnteredId INT OUTPUT
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
DECLARE	@NewEnteredId INT
EXEC [dbo].[MarshallStore.Product.Insert]

    @Active = 1,
    @DateTimeCreation = N'01/01/1753 0:00:00.001',
    @DateTimeLastModification = N'01/01/1753 0:00:00.001',
    @UserCreationId = 1,
    @UserLastModificationId = 1,
     @ProductCategoryId = 1,
    @Producer = N'PutProducer',
    @Model = N'PutModel',
    @Price = 3.14,
    @Description = N'PutDescription',
    @Image1 = N'PutImage1',
    @Image2 = N'PutImage2',
    @Image3 = N'PutImage3',

@NewEnteredId = @NewEnteredId OUTPUT

SELECT @NewEnteredId AS N'@NewEnteredId'
 *
 */

--Last modification on: 31/07/2023 14:32:13

INSERT INTO [MarshallStore.Product]
(
    [Active],
    [DateTimeCreation],
    [DateTimeLastModification],
    [UserCreationId],
    [UserLastModificationId],
    [ProductCategoryId],
    [Producer],
    [Model],
    [Price],
    [Description],
    [Image1],
    [Image2],
    [Image3]
)
VALUES
(
    @Active,
    @DateTimeCreation,
    @DateTimeLastModification,
    @UserCreationId,
    @UserLastModificationId,
    @ProductCategoryId,
    @Producer,
    @Model,
    @Price,
    @Description,
    @Image1,
    @Image2,
    @Image3
)

SELECT @NewEnteredId = @@IDENTITY