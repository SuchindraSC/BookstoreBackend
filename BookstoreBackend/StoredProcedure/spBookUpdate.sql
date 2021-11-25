CREATE PROC spBookUpdate
	@id INT,
	@bookName VARCHAR(255),
	@author VARCHAR(255), 
	@description VARCHAR(255), 
	@bookImage VARCHAR(255), 
	@quantity INT, 
	@price INT, 
	@discountPrice INT, 
	@book INT = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT * FROM [Books] WHERE BookId = @id)
	BEGIN
		SET @book = @id
		UPDATE [Books] 
		SET 
			bookName = CASE WHEN @bookName = '' THEN bookName ELSE @bookName END,
			author = CASE WHEN @author = '' THEN author ELSE @author END, 
			description = CASE WHEN @description = '' THEN description ELSE @description END, 
			bookImage = CASE WHEN @bookImage = '' THEN bookImage ELSE @bookImage END,
			quantity = @quantity, 
			price = CASE WHEN @price = '0' THEN price ELSE @price END, 
			discountPrice = CASE WHEN @discountPrice = '0' THEN discountPrice ELSE @discountPrice END 
		WHERE
			BookId = @id;
	END
	ELSE
	BEGIN
		SET @book = NULL;
	END
END