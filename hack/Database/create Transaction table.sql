CREATE TABLE [dbo].[Transaction]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[TransactionId] nvarchar(50) NOT NULL,
	[PayerEdrpo] nvarchar(10) NOT NULL,
	[Price] money NOT NULL,
	[Date] datetime NOT NULL,
	[CategoryId] nvarchar(50) NULL,
	[ReceiverTitle] nvarchar(500) NULL
)
