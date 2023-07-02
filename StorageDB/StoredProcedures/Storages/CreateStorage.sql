CREATE PROCEDURE [dbo].[sp_CreateStorage]
	@Name VARCHAR(50)
AS
	INSERT INTO [dbo].Storage (StorageName,Id) VALUES (@Name,NEWID())
RETURN 0
