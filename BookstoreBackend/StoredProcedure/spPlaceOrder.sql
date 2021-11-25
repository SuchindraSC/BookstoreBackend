create table Orders
(
	OrderId int not null identity (1,1) primary key,
	UserId int,
	BookId int,
	QuantityToBuy int,
	DateOfOrder nvarchar(20),
);

Drop Table Orders

ALTER TABLE Orders ADD CONSTRAINT Orders_ProductId_Fk
FOREIGN KEY (BookId) REFERENCES [Books] (BookId)

ALTER TABLE Orders ADD CONSTRAINT Orders_UserId_Fk
FOREIGN KEY (UserId) REFERENCES [Users] (CustomerId)

ALTER PROCEDURE spPlaceOrder(
	@BookId INT,
	@UserId INT,
	@QuantityToBuy INT,
	@OrderDate VARCHAR(20),
	@result INT OUTPUT
)
AS
BEGIN
	BEGIN TRY
			BEGIN
				INSERT INTO Orders (UserId, BookId, QuantityToBuy, DateOfOrder)
                   VALUES (@UserId, @BookId, @QuantityToBuy, @OrderDate);
				   
				   DELETE FROM [dbo].[Cart] where UserId = @UserId AND BookId = @BookId
				   SET @result=1
			END
	END TRY
	BEGIN CATCH
		SET @result=0;
	END CATCH
END