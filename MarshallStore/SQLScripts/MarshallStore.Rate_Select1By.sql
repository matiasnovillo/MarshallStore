CREATE PROCEDURE [dbo].[MarshallStore.Rate.Select1ByRateId]
(
    @RateId INT
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
EXEC [dbo].[Rate.Select1ByRateId]
    @RateId = 1
 *
 */

--Last modification on: 31/07/2023 14:25:32

SET DATEFORMAT DMY

SELECT
    [MarshallStore.Rate].[RateId],
    [MarshallStore.Rate].[Active],
    [MarshallStore.Rate].[DateTimeCreation],
    [MarshallStore.Rate].[DateTimeLastModification],
    [MarshallStore.Rate].[UserCreationId],
    [MarshallStore.Rate].[UserLastModificationId],
    [MarshallStore.Rate].[ProductId],
    [MarshallStore.Rate].[Score]
FROM 
    [MarshallStore.Rate]
    LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserCreationId] ON [MarshallStore.Rate].[UserCreationId] = [CMSCore.User.UserCreationId].[UserId]
	LEFT OUTER JOIN [CMSCore.User] AS [CMSCore.User.UserLastModificationId] ON [MarshallStore.Rate].[UserLastModificationId] = [CMSCore.User.UserLastModificationId].[UserId]
WHERE 
    1 = 1
    AND [MarshallStore.Rate].[RateId] = @RateId
ORDER BY 
    [MarshallStore.Rate].[RateId]