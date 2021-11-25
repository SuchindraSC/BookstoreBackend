ALTER procedure spGetCustomerFeedback
   @bookid int
AS
BEGIN
    SELECT
	Users.CustomerId,c.bookId,c.feedbackId,Users.fullName,feedback,rating
	from Users 
	right join CustomerFeedback as c
	on c.UserId=Users.CustomerId where c.BookId=@bookid
END