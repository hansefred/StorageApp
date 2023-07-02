CREATE PROCEDURE [dbo].[sp_CreateArticle]
	@Name VARCHAR(50),
	@Description VARCHAR(150),
	@Storage UNIQUEIDENTIFIER NULL,
	@Createdby UNIQUEIDENTIFIER
AS
	INSERT INTO [dbo].Article (Id,ArticleName,Description,Created,Updated,CreatedBy,Storage) VALUES (NEWID(),@Name,@Description,GETDATE(),GETDATE(),@Createdby,@Storage)
RETURN 0

