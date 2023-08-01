CREATE PROCEDURE [dbo].[Examples.Example.UpdateByExampleId]
(
    @ExampleId INT,
    @Active TINYINT,
    @DateTimeCreation DATETIME,
    @DateTimeLastModification DATETIME,
    @UserCreationId INT,
    @UserLastModificationId INT,
    @Boolean TINYINT,
    @DateTime DATETIME,
    @Decimal NUMERIC(24,6),
    @Integer INT,
    @TextBasic VARCHAR(20),
    @TextEmail VARCHAR(20),
    @TextFile VARCHAR(8000),
    @TextPassword VARCHAR(20),
    @TextPhoneNumber VARCHAR(20),
    @TextTag VARCHAR(20),
    @TextTextArea VARCHAR(20),
    @TextTextEditor VARCHAR(20),
    @TextURL VARCHAR(20),
    @ForeignKeyDropDown INT,
    @ForeignKeyOption INT,
    @TextHexColour VARCHAR(6),
    @Time TIME(3),

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
EXEC [dbo].[Examples.Example.UpdateByExampleId]
    @ExampleId = 1,
    @RowsAffected = @RowsAffected OUTPUT
SELECT @RowsAffected AS N'@RowsAffected'
 *
 */

--Last modification on: 15/02/2023 16:56:40

UPDATE [Examples.Example] SET
    [Active] = @Active,
    [DateTimeCreation] = @DateTimeCreation,
    [DateTimeLastModification] = @DateTimeLastModification,
    [UserCreationId] = @UserCreationId,
    [UserLastModificationId] = @UserLastModificationId,
    [Boolean] = @Boolean,
    [DateTime] = @DateTime,
    [Decimal] = @Decimal,
    [Integer] = @Integer,
    [TextBasic] = @TextBasic,
    [TextEmail] = @TextEmail,
    [TextFile] = @TextFile,
    [TextPassword] = @TextPassword,
    [TextPhoneNumber] = @TextPhoneNumber,
    [TextTag] = @TextTag,
    [TextTextArea] = @TextTextArea,
    [TextTextEditor] = @TextTextEditor,
    [TextURL] = @TextURL,
    [ForeignKeyDropDown] = @ForeignKeyDropDown,
    [ForeignKeyOption] = @ForeignKeyOption,
    [TextHexColour] = @TextHexColour,
    [Time] = @Time
WHERE 
    1 = 1 
    AND [Examples.Example].[ExampleId] = @ExampleId 

SELECT @RowsAffected = @@ROWCOUNTected = @@ROWCOUNT