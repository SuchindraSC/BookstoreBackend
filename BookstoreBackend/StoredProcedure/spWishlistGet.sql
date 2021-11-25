ALTER PROC spWishlistGet
	@userId INT
AS
BEGIN
	SELECT 
		w.WishlistId,
		w.BookId,
		w.UserId,
		b.bookName,
		b.author,
		b.description,
		b.bookImage,
		b.quantity,
		b.price,
		b.discountPrice
	FROM [Wishlist] AS w
	LEFT JOIN [Books] AS b ON w.BookId = b.BookId
	WHERE w.UserId = @userId 
END
