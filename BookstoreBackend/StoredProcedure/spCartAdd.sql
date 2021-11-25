CREATE TABLE Cart 
(
	CartId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	BookId INT NOT NULL,
	UserId INT NOT NULL,
	quantityToBuy INT NOT NULL,
);

ALTER TABLE [Cart] ADD CONSTRAINT Cart_ProductId_Fk
FOREIGN KEY (BookId) REFERENCES [Books] (BookId)

ALTER TABLE [Cart] ADD CONSTRAINT Cart_UserId_Fk
FOREIGN KEY (UserId) REFERENCES [Users] (CustomerId)

select * from Cart


CREATE PROC spCartAdd
	@bookId INT, 
	@userId INT, 
	@cart INT = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT * FROM [Cart] WHERE BookId = @bookId AND UserId = @userId)
		SET @cart = 1;
	ELSE
	BEGIN
		IF EXISTS(SELECT * FROM [Books] WHERE BookId = @bookId)
		BEGIN
			INSERT INTO [Cart](BookId, UserId, quantityToBuy)
			VALUES (@bookId, @userId, 1)
			SET @cart = 2;
		END
		ELSE
			SET @cart = NULL;
	END
END