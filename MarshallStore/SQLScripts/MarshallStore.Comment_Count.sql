CREATE PROCEDURE [dbo].[MarshallStore.Comment.Count]

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
DECLARE	@Counter int

EXEC	@Counter = [dbo].[MarshallStore.Comment.Count]

SELECT	'Counter' = @Counter
 *
 */

--Last modification on: 31/07/2023 14:24:26

SELECT 
	COUNT(*)
FROM 
	[MarshallStore.Comment]