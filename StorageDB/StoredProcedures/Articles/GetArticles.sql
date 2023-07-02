CREATE PROCEDURE [dbo].[sp_GetArticles]
AS
	SELECT *
	FROM [dbo].Article As Article
	INNER JOIN [dbo].Storage AS Storage ON Article.Storage = Storage.Id
RETURN 0

