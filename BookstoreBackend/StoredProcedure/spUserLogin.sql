CREATE PROC spUserLogin
	@email VARCHAR(255),
	@password VARCHAR(25),
	@user INT = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT * FROM Users WHERE email = @email)
	BEGIN
		IF EXISTS(SELECT * FROM Users WHERE email = @email AND password = @password)
		BEGIN
			SET @user = 2;
		END
		ELSE
		BEGIN
			SET @user = 1;
		END
	END
	ELSE
	BEGIN
		SET @user = NULL;
	END
END