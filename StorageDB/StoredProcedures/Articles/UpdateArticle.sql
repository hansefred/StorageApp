CREATE PROCEDURE [dbo].[sp_UpdateArticle] 
	@Id UNIQUEIDENTIFIER,
	@Name VARCHAR(50),
	@Description VARCHAR(150),
	@Storage UNIQUEIDENTIFIER 
AS
	Update [dbo].Article
	Set ArticleName = @Name,
	Description = @Description,
	Updated = GETDATE(),
	Storage = @Storage
	Where Id = @Id
RETURN 0
