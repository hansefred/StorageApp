CREATE PROCEDURE [dbo].[sp_GetArticledByID]
	@ID UNIQUEIDENTIFIER
AS
	SELECT *
	FROM [dbo].Article As Article
	INNER JOIN [dbo].Storage AS Storage ON Article.Storage = Storage.Id
	Where Article.Id = @ID
RETURN 0
