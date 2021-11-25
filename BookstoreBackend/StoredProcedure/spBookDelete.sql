CREATE PROC spBookDelete
	@id INT,
	@book INT = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT * FROM [Books] WHERE BookId = @id)
	BEGIN
		DELETE FROM [Books] WHERE BookId = @id
		SET @book = 1;
	END
	ELSE
	BEGIN
		SET @book = NULL;
	END
END