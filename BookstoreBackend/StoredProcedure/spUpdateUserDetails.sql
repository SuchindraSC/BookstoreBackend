CREATE PROCEDURE spUpdateUserDetails
(
	@addressID int,
	@address varchar(255),
	@city varchar(50),
	@state varchar(50),
	@type varchar(10),
	@result int output
)
AS
BEGIN
BEGIN TRY
	If exists(Select * from UserDetails where AddressId=@addressID)
	BEGIN
		UPDATE UserDetails 
		SET 
		   Address = @address, 
		   City = @city,
		   State = @state,
		   Type = @type 
	    WHERE AddressId = @addressID;
		SET @result=1;
	END 
	ELSE
	BEGIN
		SET @result=0;
	END
END TRY
BEGIN CATCH 
	SET @result=0;
END CATCH
END