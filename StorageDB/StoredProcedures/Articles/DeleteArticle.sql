CREATE PROCEDURE [dbo].[sp_DeleteArticle]
	@Id UNIQUEIDENTIFIER
AS
	Delete [dbo].Article
	Where Id = @Id
RETURN 0
