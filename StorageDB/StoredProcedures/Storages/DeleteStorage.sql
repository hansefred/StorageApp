CREATE PROCEDURE [dbo].[sp_DeleteStorage]
	@ID UNIQUEIDENTIFIER
AS
	DECLARE @Count Int
	SET @Count = (SELECT COUNT(Id) FROM [dbo].Article Where Storage = @ID)

	IF (@Count > 1)
	RAISERROR ( 'Storage has connected Articles',1,1)


	Delete [dbo].Storage Where Id = @ID
RETURN 0
