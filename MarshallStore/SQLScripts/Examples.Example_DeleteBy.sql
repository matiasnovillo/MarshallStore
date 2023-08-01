CREATE PROCEDURE [dbo].[Examples.Example.DeleteByExampleId]
(
    @ExampleId INT,
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
EXEC [dbo].[Examples.Example.DeleteByExampleId]
    @ExampleId = 1,
    @RowsAffected = @RowsAffected OUTPUT
SELECT @RowsAffected AS N'@RowsAffected'
 *
 */

--Last modification on: 15/02/2023 16:56:40

DELETE FROM 
    [Examples.Example]
WHERE 
    1 = 1
    AND [Examples.Example].[ExampleId] = @ExampleId

SELECT @RowsAffected = @@ROWCOUNT