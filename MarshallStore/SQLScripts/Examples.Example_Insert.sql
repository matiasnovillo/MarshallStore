CREATE PROCEDURE [dbo].[Examples.Example.Insert] 
(
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
EXEC [dbo].[Examples.Example.Insert]

    @Active = 1,
    @DateTimeCreation = N'01/01/1753 0:00:00.001',
    @DateTimeLastModification = N'01/01/1753 0:00:00.001',
    @UserCreationId = 1,
    @UserLastModificationId = 1,
    @Boolean = 1,
    @DateTime = N'01/01/1753 0:00:00.001',
    @Decimal = 3.14,
    @Integer = 1,
    @TextBasic = N'PutTextBasic',
    @TextEmail = N'PutTextEmail',
    @TextFile = N'PutTextFile',
    @TextPassword = N'PutTextPassword',
    @TextPhoneNumber = N'PutTextPhoneNumber',
    @TextTag = N'PutTextTag',
    @TextTextArea = N'PutTextTextArea',
    @TextTextEditor = N'PutTextTextEditor',
    @TextURL = N'PutTextURL',
     @ForeignKeyDropDown = 1,
    @ForeignKeyOption = 1,
    @TextHexColour = AABBCC,
    @Time = N'00:00:00.001',

@NewEnteredId = @NewEnteredId OUTPUT

SELECT @NewEnteredId AS N'@NewEnteredId'
 *
 */

--Last modification on: 15/02/2023 16:56:40

INSERT INTO [Examples.Example]
(
    [Active],
    [DateTimeCreation],
    [DateTimeLastModification],
    [UserCreationId],
    [UserLastModificationId],
    [Boolean],
    [DateTime],
    [Decimal],
    [Integer],
    [TextBasic],
    [TextEmail],
    [TextFile],
    [TextPassword],
    [TextPhoneNumber],
    [TextTag],
    [TextTextArea],
    [TextTextEditor],
    [TextURL],
    [ForeignKeyDropDown],
    [ForeignKeyOption],
    [TextHexColour],
    [Time]
)
VALUES
(
    @Active,
    @DateTimeCreation,
    @DateTimeLastModification,
    @UserCreationId,
    @UserLastModificationId,
    @Boolean,
    @DateTime,
    @Decimal,
    @Integer,
    @TextBasic,
    @TextEmail,
    @TextFile,
    @TextPassword,
    @TextPhoneNumber,
    @TextTag,
    @TextTextArea,
    @TextTextEditor,
    @TextURL,
    @ForeignKeyDropDown,
    @ForeignKeyOption,
    @TextHexColour,
    @Time
)

SELECT @NewEnteredId = @@IDENTITYeredId = @@IDENTITY