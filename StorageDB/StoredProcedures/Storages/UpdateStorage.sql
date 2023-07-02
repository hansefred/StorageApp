CREATE PROCEDURE [dbo].[sp_UpdateStorage]
	@Name VARCHAR(50),
	@ID UNIQUEIDENTIFIER
AS
	Update [dbo].Storage
	Set StorageName = @Name
	Where Id = @ID
RETURN 0
