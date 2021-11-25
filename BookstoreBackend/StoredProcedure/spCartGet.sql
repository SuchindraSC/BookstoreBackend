CREATE PROC spCartGet
	@userId INT
AS
BEGIN
	SELECT 
		c.CartId,
		c.BookId,
		c.UserId,
		b.bookName,
		b.author,
		b.description,
		b.bookImage,
		b.quantity,
		b.price,
		b.discountPrice,
		c.quantityToBuy
	FROM [Cart] AS c
	LEFT JOIN [Books] AS b ON c.BookId = b.BookId
	WHERE c.UserId = @userId 
END