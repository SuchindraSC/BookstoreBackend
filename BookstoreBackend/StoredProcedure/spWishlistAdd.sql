CREATE TABLE Wishlist 
(
	WishlistId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	BookId INT NOT NULL,
	UserId INT NOT NULL,
);

-- Add Foreign key constraint
ALTER TABLE [Wishlist] ADD CONSTRAINT Wishlist_ProductId_Fk
FOREIGN KEY (BookId) REFERENCES [Books] (BookId)

ALTER TABLE [Wishlist] ADD CONSTRAINT Wishlist_UserId_Fk
FOREIGN KEY (UserId) REFERENCES [Users] (CustomerId)

CREATE PROC spWishlistAdd
	@bookId INT, 
	@userId INT, 
	@wishlist INT = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT * FROM [Wishlist] WHERE BookId = @bookId AND UserId = @userId)
		SET @wishlist = 1;
	ELSE
	BEGIN
		IF EXISTS(SELECT * FROM [Books] WHERE BookId = @bookId)
		BEGIN
			INSERT INTO [Wishlist](BookId, UserId)
			VALUES (@bookId, @userId)
			SET @wishlist = 2;
		END
		ELSE
			SET @wishlist = NULL;
	END
END