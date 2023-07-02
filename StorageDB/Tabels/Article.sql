CREATE TABLE [dbo].[Article]
(
	[Id] UNIQUEIDENTIFIER  NOT NULL PRIMARY KEY,
	[ArticleName] VARCHAR(50),
	[Description] VARCHAR(150),
	[Created] DateTime,
	[Updated] DateTime,
	[CreatedBy] UNIQUEIDENTIFIER,
	[Storage] UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Storage(Id)
)
