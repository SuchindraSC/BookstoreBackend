CREATE PROC spRemoveUserDetails
	@id INT,
	@result INT = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT * FROM [UserDetails] WHERE AddressId = @id)
	BEGIN
		DELETE FROM [UserDetails] WHERE AddressId = @id
		SET @result = 1;
	END
	ELSE
	BEGIN
		SET @result = NULL;
	END
END