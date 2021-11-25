CREATE TABLE Users 
(
	CustomerId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	fullName VARCHAR(255) NOT NULL,
	email VARCHAR(255) NOT NULL UNIQUE, 
	password VARCHAR(25) NOT NULL,
	phone VARCHAR(15) NOT NULL
);

CREATE PROC spUserRegisteration
	@fullName VARCHAR(255),
	@email VARCHAR(255),
	@password VARCHAR(25),
	@phone VARCHAR(15),
	@user INT = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT * FROM Users WHERE email = @email )
		SET @user = NULL;
	ELSE
		INSERT INTO Users(fullName, email, password, phone)
		VALUES (@fullName, @email, @password, @phone)
		SET @user = SCOPE_IDENTITY();
END