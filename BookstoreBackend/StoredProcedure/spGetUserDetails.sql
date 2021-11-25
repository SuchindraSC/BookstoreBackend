CREATE procedure spGetUserDetails
  @userId int

AS
BEGIN
	IF EXISTS(Select * from UserDetails where UserId=@userId)
	begin
	    select * from UserDetails where UserId=@userId
	END
END