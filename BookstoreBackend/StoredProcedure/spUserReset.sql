CREATE PROC spUserReset
	@id INT,
	@password VARCHAR(25)
AS
BEGIN
	UPDATE Users 
	SET 
		password = @password
	WHERE
		CustomerId = @id;
END
