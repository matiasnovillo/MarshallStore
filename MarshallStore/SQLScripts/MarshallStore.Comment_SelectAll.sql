CREATE PROCEDURE [dbo].[MarshallStore.Comment.SelectAll]

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
EXEC [dbo].[MarshallStore.Comment.SelectAll]
 *
 */

--Last modification on: 31/07/2023 14:24:25

SET DATEFORMAT DMY

SELECT
    [MarshallStore.Comment].[CommentId],
    [MarshallStore.Comment].[Active],
    [MarshallStore.Comment].[DateTimeCreation],
    [MarshallStore.Comment].[DateTimeLastModification],
    [MarshallStore.Comment].[UserCreationId],
    [MarshallStore.Comment].[UserLastModificationId],
    [MarshallStore.Comment].[ProductId],
    [MarshallStore.Comment].[Text]
FROM 
    [MarshallStore.Comment]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [MarshallStore.Comment].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [MarshallStore.Comment].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
ORDER BY 
    [MarshallStore.Comment].[CommentId]