CREATE PROCEDURE [dbo].[sp_CreateStorage]
	@Name VARCHAR(50)
AS
	DECLARE @NewID UNIQUEIDENTIFIER = NEWID()

	INSERT INTO [dbo].Storage (StorageName,Id) VALUES (@Name,@NewID)

	SELECT	'Return Value' = @NewID

