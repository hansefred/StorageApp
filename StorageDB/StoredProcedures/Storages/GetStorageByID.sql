CREATE PROCEDURE [dbo].[sp_GetStorageByID]
	@ID UNIQUEIDENTIFIER
AS
	SELECT Storage.Id, Storage.StorageName, '1' AS Split, Article.Id, Article.ArticleName, Article.Created, Article.CreatedBy, Article.Description, Article.Updated
	FROM [dbo].Storage AS Storage
	Left Join [dbo].Article As Article ON Article.Storage = Storage.Id
	Where Storage.Id = @ID
RETURN 0
