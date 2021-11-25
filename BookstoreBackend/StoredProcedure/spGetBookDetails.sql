ALTER PROCEDURE spGetBookDetails
  @BookId int
AS
BEGIN

     IF(EXISTS(SELECT * FROM Books WHERE BookId=@BookId))
	 BEGIN
	   SELECT * FROM Books WHERE BookId=@BookId;
   	 END
End