CREATE TABLE Books 
(
	BookId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	bookName VARCHAR(255) NOT NULL,
	author VARCHAR(255) NOT NULL, 
	description VARCHAR(255) NOT NULL, 
	bookImage VARCHAR(255) NOT NULL, 
	quantity INT NOT NULL, 
	price INT NOT NULL, 
	discountPrice INT NOT NULL, 
);


SELECT * FROM [Books]

-- Create Stored procedure with BookModel input and BookId output
CREATE PROC spBookAdd
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
	IF EXISTS(SELECT * FROM [Books] WHERE bookName = @bookName )
		SET @book = NULL;
	ELSE
		INSERT INTO [Books](bookName, author, description, bookImage, quantity, price, discountPrice)
		VALUES (@bookName, @author, @description, @bookImage, @quantity, @price, @discountPrice)
		SET @book = SCOPE_IDENTITY();
END
