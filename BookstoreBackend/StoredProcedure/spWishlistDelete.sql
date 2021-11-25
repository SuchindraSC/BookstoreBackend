CREATE PROC spWishlistDelete
	@id INT,
	@wishlist INT = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT * FROM [Wishlist] WHERE WishlistId = @id)
	BEGIN
		DELETE FROM [Wishlist] WHERE WishlistId = @id
		SET @wishlist = 1;
	END
	ELSE
	BEGIN
		SET @wishlist = NULL;
	END
END