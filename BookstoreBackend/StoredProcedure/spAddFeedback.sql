CREATE TABLE CustomerFeedback(
	feedbackId int not null identity(1,1) PRIMARY KEY,
    userId INT,
    bookId INT,
    feedback NVARCHAR(1000),
    rating FLOAT
);

ALTER TABLE [CustomerFeedback] ADD CONSTRAINT CustomerFeedback_ProductId_Fk
FOREIGN KEY (BookId) REFERENCES [Books] (BookId)

ALTER TABLE [CustomerFeedback] ADD CONSTRAINT CustomerFeedback_UserId_Fk
FOREIGN KEY (UserId) REFERENCES [Users] (CustomerId)

ALTER PROCEDURE spAddFeedback
(	
	@BookId int,
	@UserId int ,
	@FeedBack nvarchar (1000),
	@Rating float
)	
AS
	BEGIN
		INSERT into CustomerFeedback(
		userId,
		bookId,
		feedback,
		rating
		)

		VALUES(
		@UserId,
		@BookId,
		@FeedBack,
		@Rating
		)
		declare @Average float
		select @Average = AVG(rating) from CustomerFeedback WHERE BookId = @BookId
		UPDATE Books SET rating = @Average WHERE BookId = @BookId
		declare @Count int
		select @Count = COUNT(rating) from CustomerFeedback WHERE BookId = @BookId
		UPDATE Books SET count = @Count WHERE BookId = @BookId
	END
RETURN 0