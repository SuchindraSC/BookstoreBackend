CREATE TABLE UserDetails(
	AddressId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	UserId int NOT NULL,
	Address VARCHAR(500) NOT NULL,
	City VARCHAR(50) NOT NULL,
	State VARCHAR(50) NOT NULL,
	Type varchar(10) NOT NULL 
);

ALTER TABLE [UserDetails] ADD CONSTRAINT UserDetails_UserId_Fk
FOREIGN KEY (UserId) REFERENCES [Users] (CustomerId)


CREATE PROCEDURE spAddUserAddress
(
	@userId int,
	@address varchar(600),
	@city varchar(50),
	@state varchar(50),
	@type varchar(10)
)
AS
BEGIN
INSERT INTO UserDetails(UserId, Address, City, State, Type)
values(@userId, @address, @city, @state, @type);
END