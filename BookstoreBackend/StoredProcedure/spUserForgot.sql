CREATE PROC spUserForgot
	@email VARCHAR(255),
	@user INT = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT * FROM Users WHERE email = @email)
	BEGIN
		SELECT @user = CustomerId FROM Users WHERE email = @email;
	END
	ELSE
	BEGIN
		SET @user = NULL;
	END
END