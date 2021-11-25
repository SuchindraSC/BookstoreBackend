CREATE PROC spCartUpdate
	@id INT,
	@quantity INT,
	@cart INT = NULL OUTPUT
AS
BEGIN
SET NOCOUNT ON;
	IF EXISTS(SELECT * FROM [Cart] WHERE CartId = @id)
	BEGIN
		SET @cart = 1;
		UPDATE Cart 
		SET 
			quantityToBuy = @quantity
		WHERE
			CartId = @id;
	END
	ELSE
	BEGIN
		SET @cart = NULL;
	END
END