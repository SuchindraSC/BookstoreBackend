ALTER PROC spGetOrder
(
	@userId INT
)
AS
BEGIN
	SELECT 
	b.BookId,
	b.bookName,
	b.author,
	b.description,
	b.price,
	b.discountPrice,
	b.bookImage,
	o.OrderId,
	o.UserId,
	o.QuantityToBuy,
	o.DateOfOrder
FROM [Books] AS b
RIGHT JOIN [Orders] AS o ON o.BookId = b.BookId where o.UserId = @userId
END